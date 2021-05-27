using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    public float dist = 20f; // 카메라와의 거리
    public float height = 5f; // 카메라의 높이
    public float smoothRotate = 5.0f;
    private Transform tr;

    void Start()
    {
        tr = GetComponent<Transform>();
    }

    void LateUpdate()
    {       
        //부드러운 회전을 위한 Mathf.LerpAngle
        float currYAngle = Mathf.LerpAngle(tr.eulerAngles.y, target.eulerAngles.y, smoothRotate * Time.deltaTime);
        
        //눈송이 뒤로 이동할 때(화면바라보고 이동할 때) 카메라 회전 제한
        if(MainPlayer.direction.y <= 0.1f)
            currYAngle = tr.eulerAngles.y;
        
        // 오일러 타입을 쿼터니언으로 바꾸기(이동할 앵글을 회전으로 변환)
        Quaternion rot = Quaternion.Euler(0, currYAngle, 0);

        // 카메라 위치 타겟 회전각도만큼 회전 후, dist만큼 띄우고, height만큼 높이 올리기
        tr.position = target.position - (rot * Vector3.forward * dist) + (Vector3.up * height);
        

        // 타겟 바라보게
        tr.LookAt(target);
    }
}
