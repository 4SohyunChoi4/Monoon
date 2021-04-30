using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;


public class LobbyManager : MonoBehaviour
{
    public static LobbyManager instance;
    public void SignoutButton()
    {
        FirebaseManager.instance.auth.SignOut();
        Debug.Log($"Signed Out");
        AuthGameManager.instance.ChangeScene(0);
    }


}
