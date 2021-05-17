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

    //?�증???�정??직후 ?�이?�베?�스 ?�출 ?�인?�담???�동로그??초기??
    private void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
        StartCoroutine(CheckAutoLogin());

        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    private IEnumerator CheckAutoLogin()
    {
        //???�는지 모르지�??�넣?�면 ?�러?????�음
        yield return new WaitForEndOfFrame();

        //기다�??�에 ?��?가 ?�으�?리로?�한???�기?�한??
        if(user != null)
        {
            var reloadUserTask = user.ReloadAsync();

            //?�료????까�? 기다린다(?�동로그?�이)
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

            string output = "?????�는 ?�류가 발생?�습?�다.\n?�시 ?�도??주세??";

            switch (error)
            {
                case AuthError.MissingEmail:
                    output = "?�메?�을 ?�력??주세??";
                    break;
                case  AuthError.MissingPassword:
                    output = "비�?번호�??�력??주세??";
                    break;
                case  AuthError.InvalidEmail:
                    output = "?�록?��? ?��? ?�메?�입?�다.";
                    break;
                case  AuthError.WrongPassword:
                    output = "비�?번호�??�시 ?�력??주세??";
                    break;
                case AuthError.UserNotFound:
                    output = "?�록?��? ?��? ?�용???�니??";
                    break;
            }
            loginOutputText.text = output;

        }
        //문제 ?�으�??�래 진행
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
            registerOutputText.text = "?�름???�력??주세??";
        }
        else if(_password != _confirmPassword)
        {
            registerOutputText.text = "비�?번호가 ?�치?��? ?�습?�다.";
        }
        //ok
        else{
            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);

            yield return new WaitUntil(predicate: () => registerTask.IsCompleted);

            if(registerTask.Exception != null)
            {
                FirebaseException firebaseException = (FirebaseException)registerTask.Exception.GetBaseException();
                AuthError error = (AuthError)firebaseException.ErrorCode;
                string output = "?????�는 ?�류가 발생?�습?�다. ?�시 ?�도??주세??";

                switch (error)
                {
                    case AuthError.InvalidEmail:
                        output = "?�못???�메?�을 ?�력?�셨?�니??";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        output = "?��? ?�용중인 ?�메?�입?�다.";
                        break;
                    case AuthError.WeakPassword:
                        output = "보안??취약??비�?번호?�니??";
                        break;
                    case AuthError.MissingEmail:
                        output = "?�메?�을 ?�력??주세??";
                        break;
                    case AuthError.MissingPassword:
                        output = "비�?번호�??�력??주세??";
                        break;
                }
                registerOutputText.text = output;
            }

            //?�의 ?�보�??�게 ?�력??경우 ?�네???�정?�기
            else
            {
                UserProfile profile = new UserProfile
                {
                    DisplayName = _username,

                    //todo: give profile defaul photo

                };

                var defaultUserTask = user.UpdateUserProfileAsync(profile);
                
                yield return new WaitUntil(predicate: () => defaultUserTask.IsCompleted);
                

                //?��??�보???�떤 문제가 ?�으�?
                if(defaultUserTask.Exception != null)
                {
                    //가??먼�?, ?��??�로?�을 ??��?�다.
                    user.DeleteAsync();
                    FirebaseException firebaseException = (FirebaseException)defaultUserTask.Exception.GetBaseException();
                    AuthError error = (AuthError)firebaseException.ErrorCode;
                    string output = "?????�는 ?�류가 발생?�습?�다. ?�시 ?�도??주세??";

                    switch (error)
                    {
                        case AuthError.Cancelled:
                            output = "Update User Canceled.";
                            break;
                        case AuthError.SessionExpired:
                            output = "?�션??만료?�었?�니??";
                            break;
                    }
                    registerOutputText.text = output;
                }

                //?��? ?�보 불러?�기??문제가 ?�으�?
                else
                {
                    Debug.Log($"Firebase User Created Successfully: {user.DisplayName} ({user.UserId})");
                    

                    //todo: send verification email
                    StartCoroutine(SendVerificationEmail());
                }
            }
        }
    }

    //?�메???�증 만들�?
    private IEnumerator SendVerificationEmail()
    {
        //?��? ?�는지 먼�? ?�인-> 기다�?
        if(user != null)
        {
            
            var emailTask = user.SendEmailVerificationAsync();

            yield return new WaitUntil(predicate: () => emailTask.IsCompleted);

            //?�류?�으�?
            if(emailTask.Exception != null)
            {
                FirebaseException firebaseException = (FirebaseException)emailTask.Exception.GetBaseException();
                AuthError error = (AuthError)firebaseException.ErrorCode;

                string output = "?????�는 ?�류가 발생?�습?�다. ?�시 ?�도??주세??";

                switch(error)
                {
                    case AuthError.Cancelled:
                        output = "?�증??취소?�었?�니??";
                        break;
                    case AuthError.InvalidRecipientEmail:
                        output = "?�인?��? ?�는 ?�메?�입?�다.";
                        break;
                    case AuthError.TooManyRequests:
                        output = "?��? 메일??발송?�었?�니??";
                        break;
                }
                AUIManager.instance.AwaitVerification(false, user.Email, output);
            }
            else{
                AUIManager.instance.AwaitVerification(true, user.Email, null);
                //output?�시?�됨. true=?�메??보내졌음
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
            resetPWOutputText.text = "?�명 ?�메?�의 ?�이?��? ?�력??주세??";
        }
        

      else
      {
            var resetTask = auth.SendPasswordResetEmailAsync(_email);

            yield return new WaitUntil(predicate: () => resetTask.IsCompleted);

            if(resetTask.IsCanceled){
                Debug.Log("Password reset was canceled.");
                
            } else if (resetTask.IsFaulted) {
                Debug.Log("unregisterd");
                resetPWOutputText.text = "?�록?��? ?��? 계정?�니??\n?�명 ?�메??계정???�인??주세??";
            } else if (resetTask.IsCompleted) {
                resetPWOutputText.text = "";
                AUIManager.instance.AwaitVerification(true, _email, null);
                Debug.Log("Reset passowrd Email sent Successfully");
            }
            
            //?�록???�메???�닐 경우 IsFaulted 반환
            //
            /*
            if(resetTask.Exception != null)
            {
                
                if(resetTask.IsFaulted) resetPWOutputText.text = "?�록?��? ?��? 계정?�니??";

                FirebaseException firebaseException = (FirebaseException)resetTask.Exception.GetBaseException();
                AuthError error = (AuthError)firebaseException.ErrorCode;

                string output = "?????�는 ?�류가 발생?�습?�다. ?�시 ?�도??주세??";
                
                switch(error)
                {
                    case AuthError.Cancelled:
                        output = "?�증??취소?�었?�니??";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        output = "?�인?��? ?�는 ?�메?�입?�다.";
                        break;
                    case AuthError.TooManyRequests:
                        output = "?��? 메일??발송?�었?�니??";
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

