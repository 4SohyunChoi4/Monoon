using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAutoRotate : MonoBehaviour
{
    public float rotateSpeed = 30;

    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World); //up -> y�����θ� ���� ��������
        //Rotate(Vector3) : �Ű����� �������� ȸ����Ű�� �Լ�
        //Space.World => world��ǥ�踦 �߽������� ���� 
    }

}
