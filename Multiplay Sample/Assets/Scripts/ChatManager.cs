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
    private bool DisableSend;

    private Text ChatLog;

    public void Awake()
    {
        ChatInputField = GameObject.Find("ChatInputField").GetComponent<InputField>();
        ChatLog = GameObject.Find("ChatLog").GetComponent<Text>();
    }

    private void Update()
    {
        if (photonView.isMine)
        {
            if (!DisableSend && ChatInputField.isFocused)
            {
                if (ChatInputField.text != "" && ChatInputField.text.Length > 0 && Input.GetKeyDown(KeyCode.Space))
                {
                    photonView.RPC("SendMessage", PhotonTargets.AllBuffered, ChatInputField.text);
                    photonView.RPC("ReceiveMessage", PhotonTargets.AllBuffered, PhotonNetwork.player.NickName + ": " + ChatInputField.text);
                    BubbleSpeechObject.SetActive(true);
                    ChatInputField.text = "";
                    DisableSend = true;
                }
            }
        }
        else { return; }
    }


    [PunRPC]
    private void SendMessage(string message)
    {
        UpdatedText.text = message;
        StartCoroutine("Remove");
    }

    [PunRPC]
    private void ReceiveMessage(string message)
    {
        ChatLog.text += "\n" + message;
    }

    IEnumerator Remove()
    {
        yield return new WaitForSeconds(4f);
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

}
