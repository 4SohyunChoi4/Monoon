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

    [Header("Firebase")]
    public GameObject SignoutButton;
    public GameObject DeleteUserButton;

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
                if (!EventSystem.current.IsPointerOverGameObject()) // UI ��ġ �� ���� ����
                {
                    if (hit.collider.tag == "building") //building��� tag�� ���� ��ü�� Ŭ���ϸ�
                    {
                        buildingName = hit.collider.gameObject.name;// �ش� ��ü�� �̸��� ������
                        Debug.Log(buildingName);
                        activeChatroomButton = true;
                        chatRoomButtonText.text = buildingName + " �����ϱ�";
                    }
                    else if(hit.collider.tag == "road")
                    {
                        activeChatroomButton = false;
                    }
                }
            }
        }
        if (activeChatroomButton) { chatRoomButton.SetActive(true); }
        else if(!activeChatroomButton)
        { chatRoomButton.SetActive(false); }
    }

    public void CreateGame()
    {
        PhotonNetwork.CreateRoom(buildingName, new RoomOptions() { maxPlayers = 5 }, null);
    }

    public void JoinGame()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 5;
        if (buildingName.Equals("��Ű�") || buildingName.Equals("������"))
        {
            PhotonNetwork.JoinOrCreateRoom(buildingName, roomOptions, TypedLobby.Default);
            activeChatroomButton = true;
            chatRoomButtonText.text = "�������Դϴ�..";
        }
    }

    private void OnJoinedRoom()
    {
        if(buildingName.Equals("��Ű�")) PhotonNetwork.LoadLevel("Myeongsin");
        else if(buildingName.Equals("������")) PhotonNetwork.LoadLevel("Jinlee");
        activeChatroomButton = false;
    }

    public void Signout()  //�α׾ƿ�
    {
        Debug.Log($"Click Signed Out");
        FirebaseManager.instance.auth.SignOut();
        Debug.Log($"Signed Out");
        SceneManager.LoadScene("Auth Scene");
    }

        public void DeleteUser()  //ȸ�� Ż��
    {   
        FirebaseManager.instance.auth.CurrentUser.DeleteAsync();
        Debug.Log($"Ż��: ${FirebaseManager.user.DisplayName}"); //�Ƹ��� null
        SceneManager.LoadScene("Auth Scene");

    }

    /*private void Start() // ���� ���� �� �� �� ȣ��
    {
        UsernameMenu.SetActive(true); // UsernameMeun �г� Ȱ��ȭ
    }*/

    /*(public void ChanseUserNameInput() // UsernameInput ����
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
    }*/

}
