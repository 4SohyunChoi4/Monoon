using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CurrentTime : MonoBehaviour
{
    [SerializeField] Text time;

    void Update()
    {
        int hour = DateTime.Now.Hour;
        string mClass = "";
        if (hour >= 8 && hour <= 22)
        {
            mClass = (hour - 8) + "����";
        }
        time.text = DateTime.Now.ToString("tt h : mm\n" +mClass);
        //������ ���� �޵��� ������ ����

    }
}
