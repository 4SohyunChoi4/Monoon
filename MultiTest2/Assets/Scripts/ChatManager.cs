using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class ChatManager : MonoBehaviour
{
    public Player plMove;
    public PhotonView photonView;
    public GameObject BubbleSpeechObject;
    public Text UpdatedText;

    private InputField ChatInputField;
    private Button SendButton;
    private bool DisableSend;

    private bool PrivateMessage;
    private bool PrivateTarget;

    private Text ChatLog;
    private Dropdown ChatList;
    private List<string> dropdownOptions = new List<string>();

    private bool IsDropdown;
    private string tempMessage;

    public void Awake()
    {
        ChatInputField = GameObject.Find("ChatInputField").GetComponent<InputField>();
        ChatLog = GameObject.Find("ChatLog").GetComponent<Text>();
        SendButton = GameObject.Find("SendButton").GetComponent<Button>();
        SendButton.onClick.AddListener(ClickSendButton);
        PrivateMessage = false;

        ChatList = GameObject.Find("ChatList").GetComponent<Dropdown>();
        ChatList.onValueChanged.AddListener(DropdownValueChange);
        IsDropdown = false;

        tempMessage = "";
    }

    public void Update()
    {
        if (GameObject.Find("Dropdown List") && !IsDropdown)
        {
            //Debug.Log("��Ӵٿ� Ŭ��!");
            DropdownOpen();
            IsDropdown = true;
        }
        else if (!GameObject.Find("Dropdown List"))
        {
            IsDropdown = false;
        }
    }

    [PunRPC]
    private void SendMessage(string message, PhotonMessageInfo info)
    {
        UpdatedText.text = message;
        StartCoroutine("Remove");
        //Debug.Log(ChatList.options[ChatList.value].text);
        
    }

    [PunRPC]
    private void ReceiveMessage(string message, PhotonMessageInfo info)
    {
        PrivateTarget = false;
        PrivateMessage = false;
        string[] s;
        s = message.Split(' ');
        //Debug.Log(message.Replace(s[0] + " ", "").Substring(0, 1));

        foreach (PhotonPlayer p in PhotonNetwork.playerList)
        {
            if (message.Contains("/" + p.NickName + " ") && message.Replace(s[0] + " ", "").Substring(0,1).Equals("/")) // �ӼӸ��� ���¸� ��� �ִ��� �˻�
            {
                //Debug.Log(message);
                PrivateMessage = true;
                if (message.Contains("/" + PhotonNetwork.playerName + " ")) // �ӼӸ��� Ÿ���� ������ �˻�
                {
                    PrivateTarget = true;
                    break;
                }
            }
            else
            {
            }
        }

        if (PrivateMessage)// �ӼӸ��̶��
        {
            if (info.sender.ID.Equals(PhotonNetwork.player.ID)) // ���� ���´ٸ�
            { 
                if (PrivateTarget)// ����� ������ �˻� (���� ������ ���� -> ��ü ä�ð� �����ϰ� ����)
                {
                    message = message.Replace(s[1] + " ", "");
                    message = "[��]" + message;
                    ChatLog.text += "\n" + message;
                    Debug.Log("���� ������ ����/privateMessage : " + PrivateMessage + "/privatetarget : " + PrivateTarget);  
                }
                else // ���� ������ ���� -> �� ä�ÿ��� ȸ������ ǥ��
                {
                    message = message.Replace(s[1] + " ", "");
                    message = "[��]" + message;
                    ChatLog.text += "\n" + message;
                    Debug.Log("���� ������ ����/privateMessage : " + PrivateMessage + "/privatetarget : " + PrivateTarget);
                }
            }
            else // ���� ������ �ʾҴٸ�
            {
                if (PrivateTarget)// ����� ������ �˻� -> �� ä�ÿ��� ȸ������ ǥ��
                {
                    message = message.Replace(s[1] + " ", "");
                    message = "[��]" + message;
                    ChatLog.text += "\n" + message;
                    Debug.Log("���� �Ⱥ����� �Ӹ� ����/privateMessage : " + PrivateMessage + "/privatetarget : " + PrivateTarget);
                }
                else // �޴� ����� ���� �ƴ� -> ǥ�� X
                {
                    // �޽����� ���� ����
                    Debug.Log("���� �Ⱥ����� �Ӹ� �ȹ���/privateMessage : " + PrivateMessage + "/privatetarget : " + PrivateTarget);
                }
            }
        }
        else // �ӼӸ��� �ƴ� ��ü ä���̶��
        {
            Debug.Log("�Ϲ� ä��/privateMessage : " + PrivateMessage + "/privatetarget : " + PrivateTarget);
            ChatLog.text += "\n" + "<color=black>" + message + "</color>";
        }
    }

    IEnumerator Remove()
    {
        yield return new WaitForSeconds(3f);
        BubbleSpeechObject.SetActive(false);
        DisableSend = false;
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(BubbleSpeechObject.active);
        }
        else if (stream.isReading)
        {
            BubbleSpeechObject.SetActive((bool)stream.ReceiveNext());
        }
    }

    public void ClickSendButton() // ���� ��ư Ŭ�� ��
    {
        PrivateTarget = false;
        PrivateMessage = false;
        if (photonView.isMine)
        {
            if (!DisableSend)
            {
                if (ChatInputField.text != "" && ChatInputField.text.Length > 0)/*&& Input.GetKeyDown(KeyCode.RightShift)) || (ChatInputField.touchScreenKeyboard.status == TouchScreenKeyboard.Status.Done))*/
                {
                    photonView.RPC("SendMessage", PhotonTargets.All, ChatInputField.text);
                    if (!ChatList.options[ChatList.value].text.Equals("��ü"))
                    {
                        tempMessage = "/" + ChatList.options[ChatList.value].text + " " + ChatInputField.text;
                        Debug.Log(tempMessage);
                    }
                    else
                    {
                        tempMessage = ChatInputField.text;
                    }
                    photonView.RPC("ReceiveMessage", PhotonTargets.All, PhotonNetwork.player.NickName + ": " + tempMessage);
                    //photonView.RPC("ReceiveMessage", PhotonTargets.All, PhotonNetwork.player.NickName + ": " + ChatInputField.text);
                    if (!PrivateMessage) BubbleSpeechObject.SetActive(true);
                    ChatInputField.text = "";
                    DisableSend = true;
                    
                }
            }
        }
        else { return; }
    }

    public void DropdownValueChange(int value)
    {
        //Debug.Log(ChatList.options[ChatList.value].text);
    }

    public void DropdownOpen()
    {
        ChatList.ClearOptions();
        dropdownOptions.Clear();
        dropdownOptions.Add("��ü");
        foreach (PhotonPlayer p in PhotonNetwork.playerList)
        {
            if(!p.NickName.Equals(PhotonNetwork.playerName)) // ê ����Ʈ�� �� �г����� ������ �������ִ� �÷��̾��� �г��� �߰�
             dropdownOptions.Add(p.NickName);
        }
        ChatList.AddOptions(dropdownOptions);
    }
}
