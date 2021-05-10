using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyRoom : MonoBehaviour
{
     void onButton()
    {
        SceneManager.LoadScene("MyRoom");
    }
    public void onExitButton()
    {
        SceneManager.LoadScene("UI Scene");
    }
}
