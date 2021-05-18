using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAutoRotate : MonoBehaviour
{
    public float rotateSpeed = 30;

    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World); //up -> y축으로만 가는 단위벡터
        //Rotate(Vector3) : 매개변수 기준으로 회전시키는 함수
        //Space.World => world좌표계를 중심축으로 돈다 
    }

}
