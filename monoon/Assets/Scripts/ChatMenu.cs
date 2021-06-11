using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatMenu: MonoBehaviour
{
    public static ChatMenu instance;
    public Camera GetCamera;
    private RaycastHit hit;
    public static string Chatroom = "학생회관";
    [SerializeField] private GameObject Menu;   
    
    private void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = GetCamera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                string objectName = hit.collider.gameObject.name;
                Chatroom = objectName;
                Debug.Log(objectName);
                Menu.SetActive(true);
            }
        }

    }
    
}
    /*
 [SerializeField] private GameObject Menu;

  private void PopMenu()
  {
    Menu.SetActive(true);
  }

  private void Actiu()
  {
    ;//Menu.SetActive(true);
  }
}
/*
  }
private void SetDestination(Vector3 dest)
    {
        destination = dest;
        //animator.SetBool("isMove", true);
    }

    
}
*/
