using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    public float dist = 7f; // ī�޶���� �Ÿ�
    public float height = 5f; // ī�޶��� ����
    private Transform tr;

    void Start()
    {
        tr = GetComponent<Transform>();
    }

    void Update()
    {       
        tr.position = target.position - (Vector3.forward * dist) + (Vector3.up * height);
        //tr.Rotate(target.position, 100.0f*Time.deltaTime);
        tr.LookAt(target);
    }
}
