using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerNameText : MonoBehaviour
{

    private TMP_Text nameText;

    private void Start() {
        nameText = GetComponent<TMP_Text>();

        if (FirebaseManager.user != null)
        {
            nameText.text = $"{FirebaseManager.user.DisplayName}";
        }
        else
        {
            nameText.text = "ERROR"; //FirebaseManager.User.DisplayName = null
        }

    }
}