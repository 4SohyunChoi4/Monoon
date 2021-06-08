using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyRoom : MonoBehaviour
{
    public InventoryObjects inventory;
    public InventoryObjects equipment;
    public void onButton()
    {
        SceneManager.LoadScene("MyRoom");
        inventory.Save();
        inventory.Load();
       // equipment.Load();
    }
    public void onExitButton()
    {
        inventory.Save();
        equipment.Save();
        SceneManager.LoadScene("UI Scene");
    }
}
