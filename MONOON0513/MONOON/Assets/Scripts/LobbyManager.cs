using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{   

    [SerializeField] private string VersionName = "0.1";

    [SerializeField] private GameObject BuildingName; //�κ�α���
    [SerializeField] private GameObject ActiveButton; //��Ű��̵���ư

    [SerializeField] private GameObject StartButton;
    //[SerializeField] private Text connectionInfoText;


    void Awake()
        {
            PhotonNetwork.ConnectUsingSettings(VersionName);
        }
      private void Start() // ���� ���� �� �� �� ȣ��
        {
            BuildingName.SetActive(true); // ��Ű����� �г� Ȱ��ȭ
        }

 
    private void OnConnectedToMaster()
    {
        
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Connected");
    }
    //������� -> ��Ű����� �̵� ��ư ���� �� ����
    //(�� ���ִ��� ȭ��)

/*
    public override void OnDisconnected(DisconnectCause cause)
    {
        StartButton.SetActive(false);
        connectionInfoText.text = $"Offline : Connection Disabled {cause.ToString()}";

        PhotonNetwork.ConnectUsingSettings();
    }

    public void Connect() //�����̺�Ʈ�ƴ�, �����ӽõ�
    {
        StartButton.interactable = false;

        if(PhotonNetwork.IsConnected)
        {
            connectionInfoText.text = "Connecting to Random room...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
             connectionInfoText.text = "Offline : Connection Disabled - Try reconnectiong...";

            PhotonNetwork.ConnectUsingSettings();
        }
    }
    */
    public void SetUserName() //��Ű����� ������ �̸�����
    {
        BuildingName.SetActive(false);
        PhotonNetwork.playerName = $"{FirebaseManager.user.DisplayName}";
    }

    public void CreateGame()
    {
        PhotonNetwork.CreateRoom($"{ChatMenu.Chatroom}", new RoomOptions() { MaxPlayers = 5 }, null);
    }

    public void JoinGame()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 5;
        PhotonNetwork.JoinOrCreateRoom($"{ChatMenu.Chatroom}", roomOptions, TypedLobby.Default);
    }

    private void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("MainGame");
    }

    public static LobbyManager instance;
    public void SignoutButton()
    {
        Debug.Log($"Click Signed Out");
        FirebaseManager.instance.auth.SignOut();
        Debug.Log($"Signed Out");
        SceneManager.LoadScene("Auth Scene");
    }
    
}


