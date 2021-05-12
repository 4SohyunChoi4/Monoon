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

    private void Start() // 게임 시작 시 한 번 호출
    {
        UsernameMenu.SetActive(true); // UsernameMeun 패널 활성화
    }

    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Connected");
    }

    public void ChanseUserNameInput() // UsernameInput 설정
    {
        if (UsernameInput.text.Length >= 2) // 사용자가 Username을 두 글자 이상 입력 시
        {
            StartButton.SetActive(true); // 시작 버튼 활성화
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
