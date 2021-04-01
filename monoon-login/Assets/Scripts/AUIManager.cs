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
        loginUI.SetActive(false);
        registerUI.SetActive(false);
        verifyEmailUI.SetActive(false);
        checkingForAccountUI.SetActive(false);
        
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
            verifyEmailText.text = $"Sent Email! \nPlease Verify {_email}";
        }
        else
        {
            verifyEmailText.text = $"Email Not Sent: {_output}\nPlease Verify {_email}";
        }
    }
}
