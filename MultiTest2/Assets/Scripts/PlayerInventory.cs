using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public MouseItem mouseItem = new MouseItem();

    public InventoryObjects inventory;
    //public Animator m_animator;

    private void Start()
    {
        //m_animator = this.GetComponent<Animator>();
    }

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<GroundItem>();
        if (item)//  && m_animator && m_animator.GetBool("isItem")==false
        {
            //m_animator.SetBool("isItem", true);
           // m_animator.Play("Armature_001_pickup");
            inventory.AddItem(new Item(item.item), 1);
            inventory.Save();
            Debug.Log("인벤토리 저장됨!");
            // 일단 이동 -> 닿으면 -> 잡기 전 잡는 모션 이후 destroy가 돼야 함.
            Destroy(other.gameObject);
        }
    }


   // public void OnTriggerExit(Collider other)
   // {
        //if (m_animator)
        //{
         //m_animator.SetBool("isItem", false);
         // Debug.Log("줍기 끝");
        //}
  //  }
    /*
   private voidd Update()
   {
        if (Input.GetKey(KeyCode.Space))
         {
             inventory.Save();
             Debug.Log("인벤토리 저장됨!");
         }
         if (Input.GetKey(KeyCode.CapsLock))
         {
             inventory.Load();
             Debug.Log("인벤토리 로드됨!");
         }*/

    private void OnApplicationQuit()
    {
        //inventory.Container.Items.Clear();
        inventory.Container.Items = new InventorySlot[33];
    }

}