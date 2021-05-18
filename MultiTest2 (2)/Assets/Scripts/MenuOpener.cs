using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOpener : MonoBehaviour
{
    public GameObject SubMenu;

    public void OpenMenu()
    {
        if (SubMenu != null)
        {
            Animator animator = SubMenu.GetComponent<Animator>();

            if (animator != null) // => This components is assigned to the panel.
            {
                bool isOpen = animator.GetBool("open"); // open - > close

                animator.SetBool("open", !isOpen); //vice versa( invert state)
            }
        }
    }
}
