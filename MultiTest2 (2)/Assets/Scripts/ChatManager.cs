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
            if (message.Contains("/" + p.NickName + " ") && message.Replace(s[0] + " ", "").Substring(0,1).Equals("/")) // 귓속말의 형태를 띄고 있는지 검사
            {
                //Debug.Log(message);
                PrivateMessage = true;
                if (message.Contains("/" + PhotonNetwork.playerName + " ")) // 귓속말의 타겟이 나인지 검사
                {
                    PrivateTarget = true;
                    break;
                }
            }
            else
            {
            }
        }

        if (PrivateMessage)// 귓속말이라면
        {
            if (info.sender.ID.Equals(PhotonNetwork.player.ID)) // 내가 보냈다면
            { 
                if (PrivateTarget)// 대상이 나인지 검사 (내가 나한테 보냄 -> 전체 채팅과 동일하게 간주)
                {
                    ChatLog.text += "\n" + message;
                    PrivateMessage = false;
                    Debug.Log("내가 나한테 보냄/privateMessage : " + PrivateMessage + "/privatetarget : " + PrivateTarget);  
                }
                else // 내가 남한테 보냄 -> 내 채팅에서 회색으로 표시
                {
                    message = message.Replace(s[1] + " ", "");
                    message = "[귓]" + message;
                    ChatLog.text += "\n" + message;
                    Debug.Log("내가 남한테 보냄/privateMessage : " + PrivateMessage + "/privatetarget : " + PrivateTarget);
                }
            }
            else // 내가 보내지 않았다면
            {
                if (PrivateTarget)// 대상이 나인지 검사 -> 내 채팅에서 회색으로 표시
                {
                    message = message.Replace(s[1] + " ", "");
                    message = "[귓]" + message;
                    ChatLog.text += "\n" + message;
                    Debug.Log("내가 안보내고 귓말 받음/privateMessage : " + PrivateMessage + "/privatetarget : " + PrivateTarget);
                }
                else // 받는 사람이 내가 아님 -> 표시 X
                {
                    // 메시지를 받지 않음
                    Debug.Log("내가 안보내고 귓말 안받음/privateMessage : " + PrivateMessage + "/privatetarget : " + PrivateTarget);
                }
            }
        }
        else // 귓속말이 아닌 전체 채팅이라면
        {
            Debug.Log("일반 채팅/privateMessage : " + PrivateMessage + "/privatetarget : " + PrivateTarget);
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

    public void ClickSendButton() // 전송 버튼 클릭 시
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
