using UnityEngine;
using System.Collections;
using Firebase;
using Firebase.Auth;
using TMPro;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager instance;

    [Header("Firebase")]
    public FirebaseAuth auth;
    public FirebaseUser user;
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

    //인증이 설정된 직후 파이어베이스 호출 확인한담에 자동로그인 초기화
    private void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
        StartCoroutine(CheckAutoLogin());

        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    private IEnumerator CheckAutoLogin()
    {
        //왜 넣는지 모르지만 안넣으면 에러날 때 있음
        yield return new WaitForEndOfFrame();

        //기다린 후에 유저가 있으면 리로드한다 동기화한다.
        if(user != null)
        {
            var reloadUserTask = user.ReloadAsync();

            //완료될 때 까지 기다린다(자동로그인이)
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
            GameManager.instance.ChangeScene(1);
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


    public void LogoutButton()
    {
        auth.SignOut();
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

            string output = "알 수 없는 오류가 발생했습니다.\n다시 시도해 주세요.";

            switch (error)
            {
                case AuthError.MissingEmail:
                    output = "이메일을 입력해 주세요.";
                    break;
                case  AuthError.MissingPassword:
                    output = "비밀번호를 입력해 주세요.";
                    break;
                case  AuthError.InvalidEmail:
                    output = "등록되지 않은 이메일입니다.";
                    break;
                case  AuthError.WrongPassword:
                    output = "비밀번호를 다시 입력해 주세요.";
                    break;
                case AuthError.UserNotFound:
                    output = "등록되지 않은 사용자 입니다.";
                    break;
            }
            loginOutputText.text = output;

        }
        //문제 없으면 아래 진행
        else{
            if (user.IsEmailVerified)
            {
                yield return new WaitForSeconds(1f);
                GameManager.instance.ChangeScene(1);
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
            registerOutputText.text = "이름을 입력해 주세요.";
        }
        else if(_password != _confirmPassword)
        {
            registerOutputText.text = "비밀번호가 일치하지 않습니다.";
        }
        //ok
        else{
            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);

            yield return new WaitUntil(predicate: () => registerTask.IsCompleted);

            if(registerTask.Exception != null)
            {
                FirebaseException firebaseException = (FirebaseException)registerTask.Exception.GetBaseException();
                AuthError error = (AuthError)firebaseException.ErrorCode;
                string output = "알 수 없는 오류가 발생했습니다. 다시 시도해 주세요.";

                switch (error)
                {
                    case AuthError.InvalidEmail:
                        output = "잘못된 이메일을 입력하셨습니다.";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        output = "이미 사용중인 이메일입니다.";
                        break;
                    case AuthError.WeakPassword:
                        output = "보안에 취약한 비밀번호입니다.";
                        break;
                    case AuthError.MissingEmail:
                        output = "이메일을 입력해 주세요.";
                        break;
                    case AuthError.MissingPassword:
                        output = "비밀번호를 입력해 주세요.";
                        break;
                }
                registerOutputText.text = output;
            }

            //위의 정보를 옳게 입력한 경우 닉네임 설정하기
            else
            {
                UserProfile profile = new UserProfile
                {
                    DisplayName = _username,

                    //todo: give profile defaul photo

                };

                var defaultUserTask = user.UpdateUserProfileAsync(profile);
                
                yield return new WaitUntil(predicate: () => defaultUserTask.IsCompleted);
                

                //유저정보에 어떤 문제가 있으면
                if(defaultUserTask.Exception != null)
                {
                    //가장 먼저, 유저프로필을 삭제한다.
                    user.DeleteAsync();
                    FirebaseException firebaseException = (FirebaseException)defaultUserTask.Exception.GetBaseException();
                    AuthError error = (AuthError)firebaseException.ErrorCode;
                    string output = "알 수 없는 오류가 발생했습니다. 다시 시도해 주세요.";

                    switch (error)
                    {
                        case AuthError.Cancelled:
                            output = "Update User Canceled.";
                            break;
                        case AuthError.SessionExpired:
                            output = "세션이 만료되었습니다.";
                            break;
                    }
                    registerOutputText.text = output;
                }

                //유저 정보 불러오기에 문제가 없으면
                else
                {
                    Debug.Log($"Firebase User Created Successfully: {user.DisplayName} ({user.UserId})");
                    

                    //todo: send verification email
                    StartCoroutine(SendVerificationEmail());
                }
            }
        }
    }

    //이메일 인증 만들기
    private IEnumerator SendVerificationEmail()
    {
        //유저 있는지 먼저 확인-> 기다림
        if(user != null)
        {
            
            var emailTask = user.SendEmailVerificationAsync();

            yield return new WaitUntil(predicate: () => emailTask.IsCompleted);

            //오류있으면
            if(emailTask.Exception != null)
            {
                FirebaseException firebaseException = (FirebaseException)emailTask.Exception.GetBaseException();
                AuthError error = (AuthError)firebaseException.ErrorCode;

                string output = "알 수 없는 오류가 발생했습니다. 다시 시도해 주세요!";

                switch(error)
                {
                    case AuthError.Cancelled:
                        output = "인증이 취소되었습니다.";
                        break;
                    case AuthError.InvalidRecipientEmail:
                        output = "확인되지 않는 이메일입니다.";
                        break;
                    case AuthError.TooManyRequests:
                        output = "이미 메일이 발송되었습니다.";
                        break;
                }
                AUIManager.instance.AwaitVerification(false, user.Email, output);
            }
            else{
                AUIManager.instance.AwaitVerification(true, user.Email, null);
                //output표시안됨. true=이메일 보내졌음
                Debug.Log("Email sent Successfully");
            }


        }
    }
}
