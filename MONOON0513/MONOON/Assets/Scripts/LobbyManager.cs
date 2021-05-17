using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{   

    [SerializeField] private string VersionName = "0.1";

    [SerializeField] private GameObject BuildingName; //로비로그인
    [SerializeField] private GameObject ActiveButton; //명신관이동버튼

    [SerializeField] private GameObject StartButton;
    //[SerializeField] private Text connectionInfoText;


    void Awake()
        {
            PhotonNetwork.ConnectUsingSettings(VersionName);
        }
      private void Start() // 게임 시작 시 한 번 호출
        {
            BuildingName.SetActive(true); // 명신관입장 패널 활성화
        }

 
    private void OnConnectedToMaster()
    {
        
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Connected");
    }
    //연결됐음 -> 명신관으로 이동 버튼 누를 수 있음
    //(몇 명있는지 화면)

/*
    public override void OnDisconnected(DisconnectCause cause)
    {
        StartButton.SetActive(false);
        connectionInfoText.text = $"Offline : Connection Disabled {cause.ToString()}";

        PhotonNetwork.ConnectUsingSettings();
    }

    public void Connect() //포톤이벤트아님, 룸접속시도
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
    public void SetUserName() //명신관입장 누르면 이름설정
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


