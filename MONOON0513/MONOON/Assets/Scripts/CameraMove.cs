using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    public float dist = 7f; // 카메라와의 거리
    public float height = 5f; // 카메라의 높이
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
