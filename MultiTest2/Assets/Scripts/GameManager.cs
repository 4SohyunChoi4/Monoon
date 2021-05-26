using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    private bool Off = false;

    private Text ChatLog;


    public void Awake()
    {
        ChatLog = GameObject.Find("ChatLog").GetComponent<Text>();
        SpawnPlayer();
    }

    private void Update()
    {
    }

    public void SpawnPlayer()
    {
        //float randomValue = Random.Range(-1f, 1f);

        if (PlayerPrefab == null)
        {
            Debug.LogError("playerprfab¿Ã null");
        }

        else
        {
            int randomValue = Random.Range(-5, 5);
            PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector3(randomValue, 1, 0), Quaternion.Euler(new Vector3(0, 0, 0)), 0); 
        }
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("UI Scene");
    }

    private void OnPhotonPlayerConnected(PhotonPlayer player)
    {
        ChatLog.text += "\n" + "<color=blue>" + player.name + " ¥‘¿Ã µÈæÓø¿ºÃΩ¿¥œ¥Ÿ." + "</color>";
    }

    private void OnPhotonPlayerDisconnected(PhotonPlayer player)
    {
        ChatLog.text += "\n" + "<color=red>" + player.name + " ¥‘¿Ã ≥™∞°ºÃΩ¿¥œ¥Ÿ." + "</color>";
    }
}
