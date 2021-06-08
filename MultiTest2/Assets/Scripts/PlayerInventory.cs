using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    //public MouseItem mouseItem = new MouseItem();
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

    public GameObject player;
    public static PlayerInventory instance;

    //public Animator m_animator;
    void OnLevelWasLoaded(int sceneIndex)
    {
        Debug.Log(sceneIndex);
            if( sceneIndex == 1){ //ui scene
         player.transform.position = new Vector3( 129.56f,8.69f,146.24f );

            }

        else if( sceneIndex == 5) // my room
        {
            player.transform.position = new Vector3( 2.18f, 0.02f, -2.69f );
            player.transform.rotation = Quaternion.Euler(-9.36f,201.292f,-5.338f);

        }
    }	
    void Awake()
	{
     if(instance == null)
         {    
             instance = this; // In first scene, make us the singleton.
             DontDestroyOnLoad(player);
         }
         else if(instance != this)
             Destroy(player); // On reload, singleton already set, so destroy duplicate.
     }
     /* 
     DontDestroyOnLoad (player);
     if (player != null) {
         Debug.Log("설마이건가..ㅅㅂ")
         DestroyObject(player);
     }*/
    private void Start()
    {   
        inventory.Load();
        equipment.Load();
        //DontDestroyOnLoad(inventory);
        //m_animator = this.GetComponent<Animator>();
        for (int i = 0; i < equipment.GetSlots.Length; i++)
        {
            equipment.GetSlots[i].OnBeforeUpdate += OnRemoveItem;
            equipment.GetSlots[i].OnAfterUpdate += OnAddItem;
        }
    }
    public void OnRemoveItem(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
            return;
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                print(string.Concat("삭제 ", _slot.ItemObject, "on", _slot.parent.inventory.type, "Allowed Items: "
                    , string.Join(", ", _slot.AllowedItems)));
                if (_slot.ItemObject.characterDisplay != null)
                {
                    switch (_slot.AllowedItems[0])
                    {
                        case ItemType.Helmet:
                        Destroy(helmet.gameObject);
                            break;
                        case ItemType.Face:
                        Destroy(face.gameObject);
                            break;
                        case ItemType.Weapon:
                        Destroy(weapon.gameObject);
                            break;
                        case ItemType.Shield:
                        Destroy(offhand.gameObject);
                            break;
                        case ItemType.Boots:
                        Destroy(boots.gameObject);
                            break;
                    }
                }

                break;
            case InterfaceType.Face:
                break;
        }
    }
    public void OnAddItem(InventorySlot _slot)
    {
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                print($"장착 {_slot.ItemObject} on {_slot.parent.inventory.type}, Allowed Items : {string.Join(", ", _slot.AllowedItems)}");
                if (_slot.ItemObject.characterDisplay != null)
                {
                                            Debug.Log(_slot.AllowedItems[0]);
                    switch (_slot.AllowedItems[0])
                    {
                        case ItemType.Helmet:
                            helmet = Instantiate(_slot.ItemObject.characterDisplay, helmetTransform).transform;
                            break;
                        case ItemType.Face:
                            face = Instantiate(_slot.ItemObject.characterDisplay, faceTransform).transform;
                            break;
                        case ItemType.Weapon:
                            weapon = Instantiate(_slot.ItemObject.characterDisplay, weaponTransform).transform;
                            break;
                        case ItemType.Shield:
                             offhand = Instantiate(_slot.ItemObject.characterDisplay, offhandTransform).transform;
/*
                            switch (_slot.ItemObject.type)
                            {
                                case ItemType.Weapon:
                                    offhand = Instantiate(_slot.ItemObject.characterDisplay, offHandHandTransform).transform;
                                    break;
                                case ItemType.Shield:
                                    offhand = Instantiate(_slot.ItemObject.characterDisplay, offHandWristTransform).transform;
                                    break;
                            }*/
                            break;
                        case ItemType.Boots:
                           boots = Instantiate(_slot.ItemObject.characterDisplay, bootsTransform).transform;
                            break;
                    }
                }
                break;
            case InterfaceType.Face:
                break;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<GroundItem>();
        if (item)//  && m_animator && m_animator.GetBool("isItem")==false
        {
            Item _item = new Item(item.item);
            if (inventory.AddItem(_item, 1))
            {
                Destroy(other.gameObject);
            }
            //m_animator.SetBool("isItem", true);
            // m_animator.Play("Armature_001_pickup");
            //inventory.AddItem(new Item(item.item), 1);
            //inventory.Save();
            // �ϴ� �̵� -> ������ -> ��� �� ��� ��� ���� destroy�� �ž� ��.
        }
    }
    private void OnApplicationQuit()
    {
        //  inventory.Save();
        //    equipment.Save();

        inventory.Clear();
        equipment.Clear();
    }
}

// public void OnTriggerExit(Collider other)
// {
//if (m_animator)
//{
//m_animator.SetBool("isItem", false);
// Debug.Log("�ݱ� ��");
//}
//  }
/*
private voidd Update()
{
    if (Input.GetKey(KeyCode.Space))
     {
         inventory.Save();
         Debug.Log("�κ��丮 �����!");
     }
     if (Input.GetKey(KeyCode.CapsLock))
     {
         inventory.Load();
         Debug.Log("�κ��丮 �ε��!");
     }*/


