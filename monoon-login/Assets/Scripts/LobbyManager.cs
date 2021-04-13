using UnityEngine;
using System.Collections;
using Firebase;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager instance;
  

  public void LogoutButton()
  {
    FirebaseManager.instance.auth.SignOut();
    
    GameManager.instance.ChangeScene(0);
  }
}

