using TMPro;
using UnityEngine;

public class ExGameManager : MonoBehaviour
{
 
    public static ExGameManager instance;

    // Start isAUIManager instancefirst frame update
    [Header("References")]
    [SerializeField] private GameObject MyungsinUI;
    [SerializeField] private GameObject SoonhunUI;
   /*
    [SerializeField]
    private GameObject registerUI;
    [SerializeField]
    private GameObject verifyEmailUI;
    [SerializeField]
    private TMP_Text verifyEmailText;
    [SerializeField]
    private GameObject ResetPWUI;
    */

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
        MyungsinUI.SetActive(false);
        SoonhunUI.SetActive(false);
        /*
        registerUI.SetActive(false);
        verifyEmailUI.SetActive(false);
        ResetPWUI.SetActive(false);
        */
    }


    //loginScreen: clearUI, set the login active
    public void MyungsinChatRoom()
    {
        ClearUI();
        MyungsinUI.SetActive(true);
    }

    public void SoonhunChatRoom()
    {
        ClearUI();
        SoonhunUI.SetActive(true);
    }

/*
    public void AwaitVerification(bool _emailSent, string _email, string _output)
    {
        ClearUI();
        verifyEmailUI.SetActive(true);
      
    }

    public void ResetPWScreen()
    {
        ClearUI();
        ResetPWUI.SetActive(true);
    }
    */
}
