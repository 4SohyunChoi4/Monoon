using UnityEngine;
using System.Collections;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager instance;

    [Header("Firebase")]
    public FirebaseAuth auth;
   // public Firebase.FirebaseApp app;
    public static FirebaseUser user;
    public DatabaseReference DBreference;
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

   // [SerializeField] public TMP_InputField myNoondungText;


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

    //������ ������ ���� ���̾�̽� ȣ�� Ȯ���Ѵ㿡 �ڵ��α��� �ʱ�ȭ
    private void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
        StartCoroutine(CheckAutoLogin());

        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
//        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private IEnumerator CheckAutoLogin()
    {
        //�� �ִ��� ������ �ȳ����� ������ �� ����
        yield return new WaitForEndOfFrame();

        //��ٸ� �Ŀ� ������ ������ ���ε��Ѵ� ����ȭ�Ѵ�.
        if(user != null)
        {
            var reloadUserTask = user.ReloadAsync();

            //�Ϸ�� �� ���� ��ٸ���(�ڵ��α�����)
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
        resetPWOutputText.text = "";
    }

    public void ClearLoginFeilds()
    {
        loginEmail.text="";
        loginPassword.text="";
    }
    
    public void ClearRegisterFeilds()
    {
        registerUsername.text = "";
        registerEmail.text = "";
        registerPassword.text = "";
        registerConfirmPassword.text = "";
    }

    private IEnumerator RemoveOutputs()
    {
        yield return new WaitForSeconds(3f);
        ClearOutputs();
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

            string output = "�� �� ���� ������ �߻��߽��ϴ�.\n�ٽ� �õ��� �ּ���.";

            switch (error)
            {
                case AuthError.MissingEmail:
                    output = "�̸����� �Է��� �ּ���.";
                    break;
                case  AuthError.MissingPassword:
                    output = "��й�ȣ�� �Է��� �ּ���.";
                    break;
                case  AuthError.InvalidEmail:
                    output = "��ϵ��� ���� �̸����Դϴ�.";
                    break;
                case  AuthError.WrongPassword:
                    output = "��й�ȣ�� �ٽ� �Է��� �ּ���.";
                    break;
                case AuthError.UserNotFound:
                    output = "��ϵ��� ���� ����� �Դϴ�.";
                    break;
            }
            loginOutputText.text = output;
            StartCoroutine("RemoveOutputs");

        }
        //���� ������ �Ʒ� ����
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
            registerOutputText.text = "�̸��� �Է��� �ּ���.";
            StartCoroutine("RemoveOutputs");
        }
        else if(_password != _confirmPassword)
        {
            registerOutputText.text = "��й�ȣ�� ��ġ���� �ʽ��ϴ�.";
            StartCoroutine("RemoveOutputs");
        }
        //ok
        else{
            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);

            yield return new WaitUntil(predicate: () => registerTask.IsCompleted);

            if(registerTask.Exception != null)
            {
                FirebaseException firebaseException = (FirebaseException)registerTask.Exception.GetBaseException();
                AuthError error = (AuthError)firebaseException.ErrorCode;
                string output = "�� �� ���� ������ �߻��߽��ϴ�. �ٽ� �õ��� �ּ���.";

                switch (error)
                {
                    case AuthError.InvalidEmail:
                        output = "�߸��� �̸����� �Է��ϼ̽��ϴ�.";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        output = "�̹� ������� �̸����Դϴ�.";
                        break;
                    case AuthError.WeakPassword:
                        output = "���ȿ� ����� ��й�ȣ�Դϴ�.";
                        break;
                    case AuthError.MissingEmail:
                        output = "�̸����� �Է��� �ּ���.";
                        break;
                    case AuthError.MissingPassword:
                        output = "��й�ȣ�� �Է��� �ּ���.";
                        break;
                }
                registerOutputText.text = output;
                StartCoroutine("RemoveOutputs");
            }

            //���� ������ �ǰ� �Է��� ��� �г��� �����ϱ�
            else
            {
                UserProfile profile = new UserProfile
                {
                    DisplayName = _username,

                    //todo: give profile defaul photo

                };

                var defaultUserTask = user.UpdateUserProfileAsync(profile);
                
                yield return new WaitUntil(predicate: () => defaultUserTask.IsCompleted);
                

                //���������� � ������ ������
                if(defaultUserTask.Exception != null)
                {
                    //���� ����, ������������ �����Ѵ�.
                    user.DeleteAsync();
                    FirebaseException firebaseException = (FirebaseException)defaultUserTask.Exception.GetBaseException();
                    AuthError error = (AuthError)firebaseException.ErrorCode;
                    string output = "�� �� ���� ������ �߻��߽��ϴ�. �ٽ� �õ��� �ּ���.";

                    switch (error)
                    {
                        case AuthError.Cancelled:
                            output = "Update User Canceled.";
                            break;
                        case AuthError.SessionExpired:
                            output = "������ ����Ǿ����ϴ�.";
                            break;
                    }
                    registerOutputText.text = output;
                    StartCoroutine("RemoveOutputs");
                }

                //���� ���� �ҷ����⿡ ������ ������
                else
                {
                    Debug.Log($"Firebase User Created Successfully: {user.DisplayName} ({user.UserId})");
                    

                    //todo: send verification email
                    StartCoroutine(SendVerificationEmail());
                    //StartCoroutine(UpdateUsernameDatabase($"{user.DisplayName}"));
                }
            }
        }
    }

    //�������� �����ͺ��̽�
    /*
    private IEnumerator UpdateUsernameDatabase(string _username)
    {
        var DBTask = DBreference.Child("users").Child(user.UserId).Child("username").SetValueAsync(_username);
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database username is now updated
        }
    }
    */  
    //�̸��� ���� �����
    private IEnumerator SendVerificationEmail()
    {
        //���� �ִ��� ���� Ȯ��-> ��ٸ�
        if(user != null)
        {
            
            var emailTask = user.SendEmailVerificationAsync();

            yield return new WaitUntil(predicate: () => emailTask.IsCompleted);

            //����������
            if(emailTask.Exception != null)
            {
                FirebaseException firebaseException = (FirebaseException)emailTask.Exception.GetBaseException();
                AuthError error = (AuthError)firebaseException.ErrorCode;

                string output = "�� �� ���� ������ �߻��߽��ϴ�. �ٽ� �õ��� �ּ���!";

                switch(error)
                {
                    case AuthError.Cancelled:
                        output = "������ ��ҵǾ����ϴ�.";
                        break;
                    case AuthError.InvalidRecipientEmail:
                        output = "Ȯ�ε��� �ʴ� �̸����Դϴ�.";
                        break;
                    case AuthError.TooManyRequests:
                        output = "�̹� ������ �߼۵Ǿ����ϴ�.";
                        break;
                }
                AUIManager.instance.AwaitVerification(false, user.Email, output);
            }
            else{
                AUIManager.instance.AwaitVerification(true, user.Email, null);
                //outputǥ�þȵ�. true=�̸��� ��������
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
            resetPWOutputText.text = "���� �̸����� ���̵� �Է��� �ּ���.";
            StartCoroutine("RemoveOutputs");
        }
        

      else
      {
            var resetTask = auth.SendPasswordResetEmailAsync(_email);

            yield return new WaitUntil(predicate: () => resetTask.IsCompleted);

            if(resetTask.IsCanceled){
                Debug.Log("Password reset was canceled.");
                
            } else if (resetTask.IsFaulted) {
                Debug.Log("unregisterd");
                resetPWOutputText.text = "��ϵ��� ���� �����Դϴ�.\n���� �̸��� ������ Ȯ���� �ּ���.";
            } else if (resetTask.IsCompleted) {
                resetPWOutputText.text = "";
                AUIManager.instance.AwaitVerification(true, _email, null);
                Debug.Log("Reset passowrd Email sent Successfully");
            }
            
      
        
        }           
    }

/*
    public void SaveDataButton()
    {
        //StartCoroutine(UpdateUsernameDatabase(registerUsername.text));
        
       // StartCoroutine(UpdateMyNoondung(int.Parse(myNoondungText.text)));
    }
*/
/*
    private IEnumerator UpdateMyNoondung(int _noondung)
    {
        var DBTask = DBreference.Child("users").Child(user.UserId).Child("myNoondung").SetValueAsync(_noondung);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else{
            //noondung is now updated
        }
    }
    */

    //�α׾ƿ�
    public void SignoutButton()
    {
        Debug.Log($"Click Signed Out");
        auth.SignOut();
        Debug.Log($"Signed Out");
        SceneManager.LoadScene("Auth Scene");
        ClearRegisterFeilds();
        ClearLoginFeilds();
    }

    //����Ż��
    public void DeleteUserButton()
    {   
        Debug.Log($"Click Ż���ϱ�");
        FirebaseManager.instance.auth.CurrentUser.DeleteAsync();
        Debug.Log($"Ż��: ${FirebaseManager.user.DisplayName}"); //�Ƹ��� null
        SceneManager.LoadScene("Auth Scene");

    }
}

