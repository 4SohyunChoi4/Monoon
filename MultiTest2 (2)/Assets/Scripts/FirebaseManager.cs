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

    //?¸ì¦???¤ì •??ì§í›„ ?Œì´?´ë² ?´ìŠ¤ ?¸ì¶œ ?•ì¸?œë‹´???ë™ë¡œê·¸??ì´ˆê¸°??
    private void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
        StartCoroutine(CheckAutoLogin());

        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    private IEnumerator CheckAutoLogin()
    {
        //???£ëŠ”ì§€ ëª¨ë¥´ì§€ë§??ˆë„£?¼ë©´ ?ëŸ¬?????ˆìŒ
        yield return new WaitForEndOfFrame();

        //ê¸°ë‹¤ë¦??„ì— ? ì?ê°€ ?ˆìœ¼ë©?ë¦¬ë¡œ?œí•œ???™ê¸°?”í•œ??
        if(user != null)
        {
            var reloadUserTask = user.ReloadAsync();

            //?„ë£Œ????ê¹Œì? ê¸°ë‹¤ë¦°ë‹¤(?ë™ë¡œê·¸?¸ì´)
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

            string output = "?????†ëŠ” ?¤ë¥˜ê°€ ë°œìƒ?ˆìŠµ?ˆë‹¤.\n?¤ì‹œ ?œë„??ì£¼ì„¸??";

            switch (error)
            {
                case AuthError.MissingEmail:
                    output = "?´ë©”?¼ì„ ?…ë ¥??ì£¼ì„¸??";
                    break;
                case  AuthError.MissingPassword:
                    output = "ë¹„ë?ë²ˆí˜¸ë¥??…ë ¥??ì£¼ì„¸??";
                    break;
                case  AuthError.InvalidEmail:
                    output = "?±ë¡?˜ì? ?Šì? ?´ë©”?¼ì…?ˆë‹¤.";
                    break;
                case  AuthError.WrongPassword:
                    output = "ë¹„ë?ë²ˆí˜¸ë¥??¤ì‹œ ?…ë ¥??ì£¼ì„¸??";
                    break;
                case AuthError.UserNotFound:
                    output = "?±ë¡?˜ì? ?Šì? ?¬ìš©???…ë‹ˆ??";
                    break;
            }
            loginOutputText.text = output;

        }
        //ë¬¸ì œ ?†ìœ¼ë©??„ë˜ ì§„í–‰
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
            registerOutputText.text = "?´ë¦„???…ë ¥??ì£¼ì„¸??";
        }
        else if(_password != _confirmPassword)
        {
            registerOutputText.text = "ë¹„ë?ë²ˆí˜¸ê°€ ?¼ì¹˜?˜ì? ?ŠìŠµ?ˆë‹¤.";
        }
        //ok
        else{
            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);

            yield return new WaitUntil(predicate: () => registerTask.IsCompleted);

            if(registerTask.Exception != null)
            {
                FirebaseException firebaseException = (FirebaseException)registerTask.Exception.GetBaseException();
                AuthError error = (AuthError)firebaseException.ErrorCode;
                string output = "?????†ëŠ” ?¤ë¥˜ê°€ ë°œìƒ?ˆìŠµ?ˆë‹¤. ?¤ì‹œ ?œë„??ì£¼ì„¸??";

                switch (error)
                {
                    case AuthError.InvalidEmail:
                        output = "?˜ëª»???´ë©”?¼ì„ ?…ë ¥?˜ì…¨?µë‹ˆ??";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        output = "?´ë? ?¬ìš©ì¤‘ì¸ ?´ë©”?¼ì…?ˆë‹¤.";
                        break;
                    case AuthError.WeakPassword:
                        output = "ë³´ì•ˆ??ì·¨ì•½??ë¹„ë?ë²ˆí˜¸?…ë‹ˆ??";
                        break;
                    case AuthError.MissingEmail:
                        output = "?´ë©”?¼ì„ ?…ë ¥??ì£¼ì„¸??";
                        break;
                    case AuthError.MissingPassword:
                        output = "ë¹„ë?ë²ˆí˜¸ë¥??…ë ¥??ì£¼ì„¸??";
                        break;
                }
                registerOutputText.text = output;
            }

            //?„ì˜ ?•ë³´ë¥??³ê²Œ ?…ë ¥??ê²½ìš° ?‰ë„¤???¤ì •?˜ê¸°
            else
            {
                UserProfile profile = new UserProfile
                {
                    DisplayName = _username,

                    //todo: give profile defaul photo

                };

                var defaultUserTask = user.UpdateUserProfileAsync(profile);
                
                yield return new WaitUntil(predicate: () => defaultUserTask.IsCompleted);
                

                //? ì??•ë³´???´ë–¤ ë¬¸ì œê°€ ?ˆìœ¼ë©?
                if(defaultUserTask.Exception != null)
                {
                    //ê°€??ë¨¼ì?, ? ì??„ë¡œ?„ì„ ?? œ?œë‹¤.
                    user.DeleteAsync();
                    FirebaseException firebaseException = (FirebaseException)defaultUserTask.Exception.GetBaseException();
                    AuthError error = (AuthError)firebaseException.ErrorCode;
                    string output = "?????†ëŠ” ?¤ë¥˜ê°€ ë°œìƒ?ˆìŠµ?ˆë‹¤. ?¤ì‹œ ?œë„??ì£¼ì„¸??";

                    switch (error)
                    {
                        case AuthError.Cancelled:
                            output = "Update User Canceled.";
                            break;
                        case AuthError.SessionExpired:
                            output = "?¸ì…˜??ë§Œë£Œ?˜ì—ˆ?µë‹ˆ??";
                            break;
                    }
                    registerOutputText.text = output;
                }

                //? ì? ?•ë³´ ë¶ˆëŸ¬?¤ê¸°??ë¬¸ì œê°€ ?†ìœ¼ë©?
                else
                {
                    Debug.Log($"Firebase User Created Successfully: {user.DisplayName} ({user.UserId})");
                    

                    //todo: send verification email
                    StartCoroutine(SendVerificationEmail());
                }
            }
        }
    }

    //?´ë©”???¸ì¦ ë§Œë“¤ê¸?
    private IEnumerator SendVerificationEmail()
    {
        //? ì? ?ˆëŠ”ì§€ ë¨¼ì? ?•ì¸-> ê¸°ë‹¤ë¦?
        if(user != null)
        {
            
            var emailTask = user.SendEmailVerificationAsync();

            yield return new WaitUntil(predicate: () => emailTask.IsCompleted);

            //?¤ë¥˜?ˆìœ¼ë©?
            if(emailTask.Exception != null)
            {
                FirebaseException firebaseException = (FirebaseException)emailTask.Exception.GetBaseException();
                AuthError error = (AuthError)firebaseException.ErrorCode;

                string output = "?????†ëŠ” ?¤ë¥˜ê°€ ë°œìƒ?ˆìŠµ?ˆë‹¤. ?¤ì‹œ ?œë„??ì£¼ì„¸??";

                switch(error)
                {
                    case AuthError.Cancelled:
                        output = "?¸ì¦??ì·¨ì†Œ?˜ì—ˆ?µë‹ˆ??";
                        break;
                    case AuthError.InvalidRecipientEmail:
                        output = "?•ì¸?˜ì? ?ŠëŠ” ?´ë©”?¼ì…?ˆë‹¤.";
                        break;
                    case AuthError.TooManyRequests:
                        output = "?´ë? ë©”ì¼??ë°œì†¡?˜ì—ˆ?µë‹ˆ??";
                        break;
                }
                AUIManager.instance.AwaitVerification(false, user.Email, output);
            }
            else{
                AUIManager.instance.AwaitVerification(true, user.Email, null);
                //output?œì‹œ?ˆë¨. true=?´ë©”??ë³´ë‚´ì¡ŒìŒ
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
            resetPWOutputText.text = "?™ëª… ?´ë©”?¼ì˜ ?„ì´?”ë? ?…ë ¥??ì£¼ì„¸??";
        }
        

      else
      {
            var resetTask = auth.SendPasswordResetEmailAsync(_email);

            yield return new WaitUntil(predicate: () => resetTask.IsCompleted);

            if(resetTask.IsCanceled){
                Debug.Log("Password reset was canceled.");
                
            } else if (resetTask.IsFaulted) {
                Debug.Log("unregisterd");
                resetPWOutputText.text = "?±ë¡?˜ì? ?Šì? ê³„ì •?…ë‹ˆ??\n?™ëª… ?´ë©”??ê³„ì •???•ì¸??ì£¼ì„¸??";
            } else if (resetTask.IsCompleted) {
                resetPWOutputText.text = "";
                AUIManager.instance.AwaitVerification(true, _email, null);
                Debug.Log("Reset passowrd Email sent Successfully");
            }
            
            //?±ë¡???´ë©”???„ë‹ ê²½ìš° IsFaulted ë°˜í™˜
            //
            /*
            if(resetTask.Exception != null)
            {
                
                if(resetTask.IsFaulted) resetPWOutputText.text = "?±ë¡?˜ì? ?Šì? ê³„ì •?…ë‹ˆ??";

                FirebaseException firebaseException = (FirebaseException)resetTask.Exception.GetBaseException();
                AuthError error = (AuthError)firebaseException.ErrorCode;

                string output = "?????†ëŠ” ?¤ë¥˜ê°€ ë°œìƒ?ˆìŠµ?ˆë‹¤. ?¤ì‹œ ?œë„??ì£¼ì„¸??";
                
                switch(error)
                {
                    case AuthError.Cancelled:
                        output = "?¸ì¦??ì·¨ì†Œ?˜ì—ˆ?µë‹ˆ??";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        output = "?•ì¸?˜ì? ?ŠëŠ” ?´ë©”?¼ì…?ˆë‹¤.";
                        break;
                    case AuthError.TooManyRequests:
                        output = "?´ë? ë©”ì¼??ë°œì†¡?˜ì—ˆ?µë‹ˆ??";
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

