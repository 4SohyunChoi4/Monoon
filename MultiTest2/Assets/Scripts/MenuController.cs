using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private string VersionName = "0.1";
    [SerializeField] private GameObject UsernameMenu;
    [SerializeField] private GameObject ConnectPanel;

    [SerializeField] private InputField UsernameInput;
    [SerializeField] private GameObject StartButton;

    void Awake()
    {
        PhotonNetwork.ConnectUsingSettings(VersionName);
    }

    private void Start() // ���� ���� �� �� �� ȣ��
    {
        UsernameMenu.SetActive(true); // UsernameMeun �г� Ȱ��ȭ
    }

    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Connected");
    }

    public void ChanseUserNameInput() // UsernameInput ����
    {
        if (UsernameInput.text.Length >= 2) // ����ڰ� Username�� �� ���� �̻� �Է� ��
        {
            StartButton.SetActive(true); // ���� ��ư Ȱ��ȭ
        }
        else
        {
            StartButton.SetActive(false);
        }
    }

    public void SetUserName() 
    {
        UsernameMenu.SetActive(false);
        PhotonNetwork.playerName = UsernameInput.text;
    }

    public void CreateGame()
    {
        PhotonNetwork.CreateRoom("Myung-shin", new RoomOptions() { MaxPlayers = 5 }, null);
    }

    public void JoinGame()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 5;
        PhotonNetwork.JoinOrCreateRoom("Myung-shin", roomOptions, TypedLobby.Default);
    }

    private void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("MainGame");
        Debug.Log("Connected Myung-shin");

    }
}
