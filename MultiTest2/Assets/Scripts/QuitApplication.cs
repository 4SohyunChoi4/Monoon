using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitApplication : MonoBehaviour
{
    private Button Yes;
    // Start is called before the first frame update
    void Start()
    {
        Yes = GetComponent<Button>();
        Yes.onClick.AddListener(QuitGame);
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
