using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObjects inventory;
    public Animator m_animator;

    private void Start()
    {
        m_animator = this.GetComponent<Animator>();
    }

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        if (item && m_animator && m_animator.GetBool("isItem")==false)
        {
            m_animator.SetBool("isItem", true);
           // m_animator.Play("Armature_001_pickup");
            inventory.AddItem(item.item, 1);
            inventory.Save();
            Debug.Log("�κ��丮 �����!");
            // �ϴ� �̵� -> ������ -> ��� �� ��� ��� ���� destroy�� �ž� ��.
            Destroy(other.gameObject);
        }
    }


    public void OnTriggerExit(Collider other)
    {
        //if (m_animator)
        //{
         m_animator.SetBool("isItem", false);
         // Debug.Log("�K�K�̳�");
        //}
    }
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

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }

}