using TMPro;
using UnityEngine;

public class AUIManager : MonoBehaviour
{
 
    public static AUIManager instance;

    // Start isAUIManager instancefirst frame update
    [Header("References")]
    [SerializeField]
    private GameObject checkingForAccountUI;
    [SerializeField]
    private GameObject loginUI;
    [SerializeField]
    private GameObject registerUI;
    [SerializeField]
    private GameObject verifyEmailUI;
    [SerializeField]
    private TMP_Text verifyEmailText;
    [SerializeField]
    private GameObject ResetPWUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    //ADD A private the Clearui -> add to public void
    private void ClearUI()
    {
        FirebaseManager.instance.ClearOutputs();
        checkingForAccountUI.SetActive(false);
        loginUI.SetActive(false);
        registerUI.SetActive(false);
        verifyEmailUI.SetActive(false);
        ResetPWUI.SetActive(false);
        
    }


    //loginScreen: clearUI, set the login active
    public void LoginScreen()
    {
        ClearUI();
        loginUI.SetActive(true);
    }

    public void RegisterScreen()
    {
        ClearUI();
        registerUI.SetActive(true);
    }

    public void AwaitVerification(bool _emailSent, string _email, string _output)
    {
        ClearUI();
        verifyEmailUI.SetActive(true);
        if(_emailSent)
        {
            verifyEmailText.text = $"이메일을 보냈습니다. \n메일함을 확인해 주세요.\n{_email}";
        }
        else
        {
            verifyEmailText.text = $"이메일이 전송되지 않았습니다.\n:{_output}\n메일함을 확인해 주세요.{_email}";
        }
    }

    public void ResetPWScreen()
    {
        ClearUI();
        ResetPWUI.SetActive(true);
    }
}
