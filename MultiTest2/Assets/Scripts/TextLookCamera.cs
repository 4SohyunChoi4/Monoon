using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLookCamera : MonoBehaviour
{
    private Transform mainCameraTransform; // 닉네임, 말풍선이 따라올 카메라 위치
    private Camera cameraToLookAt;

    private void Start()
    {
        //mainCameraTransform = Camera.main.transform;
        cameraToLookAt = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    private void LateUpdate()
    {
        //transform.LookAt(transform.position + mainCameraTransform.rotation * Vector3.forward, mainCameraTransform.rotation * Vector3.up);
        transform.LookAt(transform.position + cameraToLookAt.transform.rotation * Vector3.forward, cameraToLookAt.transform.rotation * Vector3.up);

    }
}
