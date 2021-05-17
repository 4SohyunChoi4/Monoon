using UnityEngine;
using System.Collections;
using Firebase;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager instance;

    [Header("Firebase")]
    public FirebaseAuth auth;
    public static FirebaseUser user;
    [Space(5f)]

    [Header("Login References")]
    [SerializeField]
    private TMP_InputField loginEmail;
    [SerializeField]
    private TMP_InputField loginPassword;
    [SerializeField]
    private TMP_Text loginOutputText;
    [Space(5f)]

    [Header("Register References")]
    [SerializeField]
    private TMP_InputField registerUsername;
    [SerializeField]
    private TMP_InputField registerEmail;
    [SerializeField]
    private TMP_InputField registerPassword;
    [SerializeField]
    private TMP_InputField registerConfirmPassword;
    [SerializeField]
    private TMP_Text registerOutputText;
    [Space(5f)]

    [Header("Reset Password References")]
    [SerializeField]
    private TMP_InputField ResetPWEmail;
    [SerializeField]
    private TMP_Text resetPWOutputText;


    private void Awake()
    {

        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
        }
    }

    private void Start()
    {
        StartCoroutine(CheckAndFixDependancies());
    }

    private IEnumerator CheckAndFixDependancies()
    {
        var CheckAndFixdependanciesTask = FirebaseApp.CheckAndFixDependenciesAsync();

        yield return new WaitUntil(predicate: () => CheckAndFixdependanciesTask.IsCompleted);

        var dependancyResult = CheckAndFixdependanciesTask.Result;

        if(dependancyResult == DependencyStatus.Available)
        {
            InitializeFirebase();
        }
        else{
            Debug.LogError($"Could not resolve all Firebase dependacncies: {dependancyResult}");
        }
    }

    //?ธ์ฆ???ค์ ??์งํ ?์ด?ด๋ฒ ?ด์ค ?ธ์ถ ?์ธ?๋ด???๋๋ก๊ทธ??์ด๊ธฐ??
    private void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
        StartCoroutine(CheckAutoLogin());

        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    private IEnumerator CheckAutoLogin()
    {
        //???ฃ๋์ง ๋ชจ๋ฅด์ง๋ง??๋ฃ?ผ๋ฉด ?๋ฌ?????์
        yield return new WaitForEndOfFrame();

        //๊ธฐ๋ค๋ฆ??์ ? ์?๊ฐ ?์ผ๋ฉ?๋ฆฌ๋ก?ํ???๊ธฐ?ํ??
        if(user != null)
        {
            var reloadUserTask = user.ReloadAsync();

            //?๋ฃ????๊น์? ๊ธฐ๋ค๋ฆฐ๋ค(?๋๋ก๊ทธ?ธ์ด)
            yield return new WaitUntil(predicate: () => reloadUserTask.IsCompleted);

            AutoLogin();

        }
        else
        {
            AUIManager.instance.LoginScreen();
        }

    }
    
    private void AutoLogin()
    {
        if(user != null)
        {
            //todo: Email verification
            if (user.IsEmailVerified)
            {
                SceneManager.LoadScene("UI Scene");
            }
            else
            {
                StartCoroutine(SendVerificationEmail());
            }
        }
        else{
            AUIManager.instance.LoginScreen();
        }
    }



    private void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if(auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;

            if(!signedIn && user != null)
            {
                Debug.Log("Signed Out");
            }

            user = auth.CurrentUser;

            if (signedIn)
            {
                Debug.Log($"Signed In: {user.DisplayName}");
            }
        }
    }
    
    public void ClearOutputs()
    {
        loginOutputText.text = "";
        registerOutputText.text = "";
    }

    public void LoginButton()
    {
        StartCoroutine(LoginLogic(loginEmail.text+"@sookmyung.ac.kr", loginPassword.text));
    }


    public void RegisterButton()
    {
        StartCoroutine(RegisterLogic(registerUsername.text, registerEmail.text+"@sookmyung.ac.kr", registerPassword.text, registerConfirmPassword.text));
    }

    private IEnumerator LoginLogic(string _email, string _password)
    {
        Credential credential = EmailAuthProvider.GetCredential(_email, _password);

        var loginTask = auth.SignInWithCredentialAsync(credential);

        yield return new WaitUntil(predicate: () => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {
            FirebaseException firebaseException = (FirebaseException)loginTask.Exception.GetBaseException();
            AuthError error = (AuthError)firebaseException.ErrorCode;

            string output = "?????๋ ?ค๋ฅ๊ฐ ๋ฐ์?์ต?๋ค.\n?ค์ ?๋??์ฃผ์ธ??";

            switch (error)
            {
                case AuthError.MissingEmail:
                    output = "?ด๋ฉ?ผ์ ?๋ ฅ??์ฃผ์ธ??";
                    break;
                case  AuthError.MissingPassword:
                    output = "๋น๋?๋ฒํธ๋ฅ??๋ ฅ??์ฃผ์ธ??";
                    break;
                case  AuthError.InvalidEmail:
                    output = "?ฑ๋ก?์? ?์? ?ด๋ฉ?ผ์?๋ค.";
                    break;
                case  AuthError.WrongPassword:
                    output = "๋น๋?๋ฒํธ๋ฅ??ค์ ?๋ ฅ??์ฃผ์ธ??";
                    break;
                case AuthError.UserNotFound:
                    output = "?ฑ๋ก?์? ?์? ?ฌ์ฉ???๋??";
                    break;
            }
            loginOutputText.text = output;

        }
        //๋ฌธ์  ?์ผ๋ฉ??๋ ์งํ
        else{
            if (user.IsEmailVerified)
            {
                yield return new WaitForSeconds(1f);
                SceneManager.LoadScene("UI Scene");
            }
            else{
                    //todo: send verification email
                    StartCoroutine(SendVerificationEmail());

                    //temporary
                    //GameManager.instance.ChangeScene(1);
            }
        }
    }

    private IEnumerator RegisterLogic(string _username, string _email, string _password, string _confirmPassword)
    {
        
        if(_username == "")
        {
            registerOutputText.text = "?ด๋ฆ???๋ ฅ??์ฃผ์ธ??";
        }
        else if(_password != _confirmPassword)
        {
            registerOutputText.text = "๋น๋?๋ฒํธ๊ฐ ?ผ์น?์? ?์ต?๋ค.";
        }
        //ok
        else{
            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);

            yield return new WaitUntil(predicate: () => registerTask.IsCompleted);

            if(registerTask.Exception != null)
            {
                FirebaseException firebaseException = (FirebaseException)registerTask.Exception.GetBaseException();
                AuthError error = (AuthError)firebaseException.ErrorCode;
                string output = "?????๋ ?ค๋ฅ๊ฐ ๋ฐ์?์ต?๋ค. ?ค์ ?๋??์ฃผ์ธ??";

                switch (error)
                {
                    case AuthError.InvalidEmail:
                        output = "?๋ชป???ด๋ฉ?ผ์ ?๋ ฅ?์จ?ต๋??";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        output = "?ด๋? ?ฌ์ฉ์ค์ธ ?ด๋ฉ?ผ์?๋ค.";
                        break;
                    case AuthError.WeakPassword:
                        output = "๋ณด์??์ทจ์ฝ??๋น๋?๋ฒํธ?๋??";
                        break;
                    case AuthError.MissingEmail:
                        output = "?ด๋ฉ?ผ์ ?๋ ฅ??์ฃผ์ธ??";
                        break;
                    case AuthError.MissingPassword:
                        output = "๋น๋?๋ฒํธ๋ฅ??๋ ฅ??์ฃผ์ธ??";
                        break;
                }
                registerOutputText.text = output;
            }

            //?์ ?๋ณด๋ฅ??ณ๊ฒ ?๋ ฅ??๊ฒฝ์ฐ ?๋ค???ค์ ?๊ธฐ
            else
            {
                UserProfile profile = new UserProfile
                {
                    DisplayName = _username,

                    //todo: give profile defaul photo

                };

                var defaultUserTask = user.UpdateUserProfileAsync(profile);
                
                yield return new WaitUntil(predicate: () => defaultUserTask.IsCompleted);
                

                //? ์??๋ณด???ด๋ค ๋ฌธ์ ๊ฐ ?์ผ๋ฉ?
                if(defaultUserTask.Exception != null)
                {
                    //๊ฐ??๋จผ์?, ? ์??๋ก?์ ?? ?๋ค.
                    user.DeleteAsync();
                    FirebaseException firebaseException = (FirebaseException)defaultUserTask.Exception.GetBaseException();
                    AuthError error = (AuthError)firebaseException.ErrorCode;
                    string output = "?????๋ ?ค๋ฅ๊ฐ ๋ฐ์?์ต?๋ค. ?ค์ ?๋??์ฃผ์ธ??";

                    switch (error)
                    {
                        case AuthError.Cancelled:
                            output = "Update User Canceled.";
                            break;
                        case AuthError.SessionExpired:
                            output = "?ธ์??๋ง๋ฃ?์?ต๋??";
                            break;
                    }
                    registerOutputText.text = output;
                }

                //? ์? ?๋ณด ๋ถ๋ฌ?ค๊ธฐ??๋ฌธ์ ๊ฐ ?์ผ๋ฉ?
                else
                {
                    Debug.Log($"Firebase User Created Successfully: {user.DisplayName} ({user.UserId})");
                    

                    //todo: send verification email
                    StartCoroutine(SendVerificationEmail());
                }
            }
        }
    }

    //?ด๋ฉ???ธ์ฆ ๋ง๋ค๊ธ?
    private IEnumerator SendVerificationEmail()
    {
        //? ์? ?๋์ง ๋จผ์? ?์ธ-> ๊ธฐ๋ค๋ฆ?
        if(user != null)
        {
            
            var emailTask = user.SendEmailVerificationAsync();

            yield return new WaitUntil(predicate: () => emailTask.IsCompleted);

            //?ค๋ฅ?์ผ๋ฉ?
            if(emailTask.Exception != null)
            {
                FirebaseException firebaseException = (FirebaseException)emailTask.Exception.GetBaseException();
                AuthError error = (AuthError)firebaseException.ErrorCode;

                string output = "?????๋ ?ค๋ฅ๊ฐ ๋ฐ์?์ต?๋ค. ?ค์ ?๋??์ฃผ์ธ??";

                switch(error)
                {
                    case AuthError.Cancelled:
                        output = "?ธ์ฆ??์ทจ์?์?ต๋??";
                        break;
                    case AuthError.InvalidRecipientEmail:
                        output = "?์ธ?์? ?๋ ?ด๋ฉ?ผ์?๋ค.";
                        break;
                    case AuthError.TooManyRequests:
                        output = "?ด๋? ๋ฉ์ผ??๋ฐ์ก?์?ต๋??";
                        break;
                }
                AUIManager.instance.AwaitVerification(false, user.Email, output);
            }
            else{
                AUIManager.instance.AwaitVerification(true, user.Email, null);
                //output?์?๋จ. true=?ด๋ฉ??๋ณด๋ด์ก์
                Debug.Log("Email sent Successfully");
            }
        }
    }
    public void ResetPWButton()
    {
        StartCoroutine(ResetPWLogic(ResetPWEmail.text+"@sookmyung.ac.kr"));
    }
   private IEnumerator ResetPWLogic(string _email)
    {
        if(_email == "@sookmyung.ac.kr")
        {
            resetPWOutputText.text = "?๋ช ?ด๋ฉ?ผ์ ?์ด?๋? ?๋ ฅ??์ฃผ์ธ??";
        }
        

      else
      {
            var resetTask = auth.SendPasswordResetEmailAsync(_email);

            yield return new WaitUntil(predicate: () => resetTask.IsCompleted);

            if(resetTask.IsCanceled){
                Debug.Log("Password reset was canceled.");
                
            } else if (resetTask.IsFaulted) {
                Debug.Log("unregisterd");
                resetPWOutputText.text = "?ฑ๋ก?์? ?์? ๊ณ์ ?๋??\n?๋ช ?ด๋ฉ??๊ณ์ ???์ธ??์ฃผ์ธ??";
            } else if (resetTask.IsCompleted) {
                resetPWOutputText.text = "";
                AUIManager.instance.AwaitVerification(true, _email, null);
                Debug.Log("Reset passowrd Email sent Successfully");
            }
            
            //?ฑ๋ก???ด๋ฉ???๋ ๊ฒฝ์ฐ IsFaulted ๋ฐํ
            //
            /*
            if(resetTask.Exception != null)
            {
                
                if(resetTask.IsFaulted) resetPWOutputText.text = "?ฑ๋ก?์? ?์? ๊ณ์ ?๋??";

                FirebaseException firebaseException = (FirebaseException)resetTask.Exception.GetBaseException();
                AuthError error = (AuthError)firebaseException.ErrorCode;

                string output = "?????๋ ?ค๋ฅ๊ฐ ๋ฐ์?์ต?๋ค. ?ค์ ?๋??์ฃผ์ธ??";
                
                switch(error)
                {
                    case AuthError.Cancelled:
                        output = "?ธ์ฆ??์ทจ์?์?ต๋??";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        output = "?์ธ?์? ?๋ ?ด๋ฉ?ผ์?๋ค.";
                        break;
                    case AuthError.TooManyRequests:
                        output = "?ด๋? ๋ฉ์ผ??๋ฐ์ก?์?ต๋??";
                        break;
                }

                AUIManager.instance.AwaitVerification(false, _email, output);
                    
               
            }
                else {
                    AUIManager.instance.AwaitVerification(true, _email, null);
                    Debug.Log("Reset passowrd Email sent Successfully");
                }
                
            }
            */
      
        
        }           
    }}

