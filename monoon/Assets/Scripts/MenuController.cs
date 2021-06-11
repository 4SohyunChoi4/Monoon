using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    [SerializeField] private string VersionName = "0.1";

    private Camera camera;
    string buildingName;
    public GameObject chatRoomButton;
    public Text chatRoomButtonText;
    private bool activeChatroomButton;

    //[SerializeField] private GameObject UsernameMenu;
    //[SerializeField] private GameObject ConnectPanel;

    //[SerializeField] private InputField UsernameInput;
    //[SerializeField] private GameObject StartButton;

    void Awake()
    {
        PhotonNetwork.ConnectUsingSettings(VersionName);
        camera = Camera.main;
        activeChatroomButton = false;
        PhotonNetwork.playerName = FirebaseManager.user.DisplayName;
    }

    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Connected");
    }

    public void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (!EventSystem.current.IsPointerOverGameObject()) // UI 터치 시 반응 방지
                {
                    if (hit.collider.tag == "building") //building라는 tag를 가진 물체를 클릭하면
                    {
                        buildingName = hit.collider.gameObject.name;// 해당 물체의 이름을 가져옴
                        Debug.Log(buildingName);
                        activeChatroomButton = true;
                        chatRoomButtonText.text = buildingName + " 입장하기";
                    }
                    else if(hit.collider.tag == "road")
                    {
                        activeChatroomButton = false;
                    }
                }
            }
        }
        if (activeChatroomButton) { chatRoomButton.SetActive(true);}
        else if(!activeChatroomButton)
        { chatRoomButton.SetActive(false); }
    }

    public void CreateGame()
    {
        PhotonNetwork.CreateRoom(buildingName, new RoomOptions() { maxPlayers = 5 }, null);
    }

    public void JoinGame()
    {
        if (buildingName.Equals("학생회관(상점)"))
        {
              SceneManager.LoadScene("Shop");
        }
        else
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.maxPlayers = 5;
            if (buildingName.Equals("명신관") || buildingName.Equals("진리관") || buildingName.Equals("순헌관") || buildingName.Equals("새힘관") || buildingName.Equals("도서관"))
            {
                PhotonNetwork.JoinOrCreateRoom(buildingName, roomOptions, TypedLobby.Default);
                activeChatroomButton = true;
                chatRoomButtonText.text = "접속중입니다..";
            }
        }
        
    }

    private void OnJoinedRoom()
    {
        if(buildingName.Equals("명신관")) PhotonNetwork.LoadLevel("Myeongsin");
        else if(buildingName.Equals("진리관")) PhotonNetwork.LoadLevel("Jinlee");
        else if (buildingName.Equals("순헌관")) PhotonNetwork.LoadLevel("Soonhun");
        else if (buildingName.Equals("새힘관")) PhotonNetwork.LoadLevel("Saeheem");
        else if (buildingName.Equals("도서관")) PhotonNetwork.LoadLevel("library");
        activeChatroomButton = false;
    }

        public void SignoutButton()
    {
        FirebaseManager.instance.SignoutButton();
    }

    //계정탈퇴
    public void DeleteUser()
    {   
        FirebaseManager.instance.DeleteUserButton();

    }

    /*private void Start() // 게임 시작 시 한 번 호출
    {
        UsernameMenu.SetActive(true); // UsernameMeun 패널 활성화
    }*/

    /*(public void ChanseUserNameInput() // UsernameInput 설정
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
    }*/

}
