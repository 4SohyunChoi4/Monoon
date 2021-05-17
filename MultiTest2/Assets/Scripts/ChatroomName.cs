using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatroomName : MonoBehaviour
{
    
    private TMP_Text roomName;
    private void Start()
    {
        roomName = GetComponent<TMP_Text>();
        roomName.text = $"{ChatMenu.Chatroom}";

    }
}
