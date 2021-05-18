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

    public void Awake()
    {
        ChatInputField = GameObject.Find("ChatInputField").GetComponent<InputField>();
        ChatLog = GameObject.Find("ChatLog").GetComponent<Text>();
        SendButton = GameObject.Find("SendButton").GetComponent<Button>();
        SendButton.onClick.AddListener(ClickSendButton);
        PrivateMessage = false;
    }

    public void Update()
    {

    }

    [PunRPC]
    private void SendMessage(string message, PhotonMessageInfo info)
    {
        UpdatedText.text = message;
        
        StartCoroutine("Remove");
    }

    [PunRPC]
    private void ReceiveMessage(string message, PhotonMessageInfo info)
    {
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
                    ChatLog.text += "\n" + message;
                    PrivateMessage = false;
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
        if (photonView.isMine)
        {
            if (!DisableSend)
            {
                if (ChatInputField.text != "" && ChatInputField.text.Length > 0)/*&& Input.GetKeyDown(KeyCode.RightShift)) || (ChatInputField.touchScreenKeyboard.status == TouchScreenKeyboard.Status.Done))*/
                {
                    photonView.RPC("SendMessage", PhotonTargets.AllBuffered, ChatInputField.text);
                    photonView.RPC("ReceiveMessage", PhotonTargets.AllBuffered, PhotonNetwork.player.NickName + ": " + ChatInputField.text);
                    if(!PrivateMessage) BubbleSpeechObject.SetActive(true);
                    ChatInputField.text = "";
                    DisableSend = true;
                }
            }
        }
        else { return; }
        PrivateTarget = false;
        PrivateMessage = false;
    }
}
