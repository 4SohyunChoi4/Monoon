using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpener : MonoBehaviour
{
    public GameObject Panel;
    public GameObject SubMenu;
    public void OpenPanel()
    {
        if (Panel != null)
        {
            //bool isActive = Panel.activeSelf;
            //Panel.SetActive(!isActive);
            Animator animator = Panel.GetComponent<Animator>();
            Animator animator2 = SubMenu.GetComponent<Animator>();
            if (animator != null && animator2 != null) // => This components is assigned to the panel.
            {
                bool isOpen = animator.GetBool("open"); // open - > close
                bool isOpen2 = animator2.GetBool("open"); // open - > close

                animator.SetBool("open", !isOpen); //vice versa( invert state)
                animator2.SetBool("open", !isOpen2); //vice versa( invert state)

            }
        }
    }

}
