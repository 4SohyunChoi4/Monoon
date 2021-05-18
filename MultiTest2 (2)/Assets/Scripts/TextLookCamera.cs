using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLookCamera : MonoBehaviour
{
    private Transform mainCameraTransform; // �г���, ��ǳ���� ����� ī�޶� ��ġ
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
