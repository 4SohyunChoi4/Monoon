using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    private bool Off = false;

    private Text ChatLog;

    private Transform helmet;
    private Transform face;
    private Transform weapon;
    private Transform offhand;
    private Transform boots;

    public Transform helmetTransform;
    public Transform faceTransform;
    public Transform weaponTransform;
    public Transform offhandTransform;
    public Transform bootsTransform;
    public InventoryObjects inventory;
    public InventoryObjects equipment;

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
            Debug.LogError("playerprfab�� null");
        }

        else
        {
            int randomValue = Random.Range(-5, 5);
            PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector3(randomValue, 1, 0), Quaternion.Euler(new Vector3(0, 0, 0)), 0); 
            for (int i = 0; i < equipment.GetSlots.Length; i++)
            {
                EquipItem;  
            }
        }
    }
/*public void EquipItem(InventorySlot _slot) //_slot
    { 
                switch (_slot.AllowedItems[0])
                    {
                        case ItemType.Helmet:
                            helmet = Instantiate(_slot.ItemObject.characterDisplay, helmetTransform).transform;
                        case ItemType.Face:
                            face = Instantiate(_slot.ItemObject.characterDisplay, faceTransform).transform;
                        case ItemType.Weapon:
                            weapon = Instantiate(_slot.ItemObject.characterDisplay, weaponTransform).transform;
                        case ItemType.Shield:
                             offhand = Instantiate(_slot.ItemObject.characterDisplay, offhandTransform).transform;
                    }
    }  
    */
/*
                            switch (_slot.ItemObject.type)
                            {
                                case ItemType.Weapon:
                                    offhand = Instantiate(_slot.ItemObject.characterDisplay, offHandHandTransform).transform;
                                    break;
                                case ItemType.Shield:
                                    offhand = Instantiate(_slot.ItemObject.characterDisplay, offHandWristTransform).transform;
                                    break;
                            }
                            break;
                        case ItemType.Boots:
                           boots = Instantiate(_slot.ItemObject.characterDisplay, bootsTransform).transform;
                            break;
            //if (GetSlots[i].item.Id != -1)
            //{
               //Debug.Log(GetSlots[i]);
            //}
        }
    }
        //for (int i = 0; i < equipment.GetSlots.Length; i++)
        //{
           // Debug.Log(equipment.GetSlots[1]);   
           // Debug.Log(equipment.GetSlots[2]);   
           // Debug.Log(equipment.GetSlots[3]);   
           // Debug.Log(equipment.GetSlots[4]);   
            //Debug.Log(equipment[1]);   
            //Debug.Log(equipment[2]);   
         //   Debug.Log(equipment[3]);   
           // Debug.Log(equipment[4]);   
         /*if(equipment[0]!=null
                                    helmet = Instantiate(_slot.ItemObject.characterDisplay, helmetTransform).transform;

        //}
        equipment[1] != null
                            face = Instantiate(_slot.ItemObject.characterDisplay, faceTransform).transform;
*/
            /*
            //case InterfaceType.Equipment:
                if (_slot.ItemObject.characterDisplay != null)
                {
                    Debug.Log(_slot.AllowedItems[0]);

                    }
                }*/
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("UI Scene");
    }

    private void OnPhotonPlayerConnected(PhotonPlayer player)
    {
        ChatLog.text += "\n" + "<color=blue>" + player.name + " 님이 입장하셨습니다." + "</color>";
    }

    private void OnPhotonPlayerDisconnected(PhotonPlayer player)
    {
        ChatLog.text += "\n" + "<color=red>" + player.name + " 님이 퇴장하셨습니다." + "</color>";
    }
}
