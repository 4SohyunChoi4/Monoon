using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    public float dist = 20f; // ī�޶���� �Ÿ�
    public float height = 5f; // ī�޶��� ����
    public float smoothRotate = 5.0f;
    private Transform tr;

    void Start()
    {
        tr = GetComponent<Transform>();
    }

    void LateUpdate()
    {       
        //�ε巯�� ȸ���� ���� Mathf.LerpAngle
        float currYAngle = Mathf.LerpAngle(tr.eulerAngles.y, target.eulerAngles.y, smoothRotate * Time.deltaTime);
        
        //������ �ڷ� �̵��� ��(ȭ��ٶ󺸰� �̵��� ��) ī�޶� ȸ�� ����
        if(MainPlayer.direction.y <= 0.1f)
            currYAngle = tr.eulerAngles.y;
        
        // ���Ϸ� Ÿ���� ���ʹϾ����� �ٲٱ�(�̵��� �ޱ��� ȸ������ ��ȯ)
        Quaternion rot = Quaternion.Euler(0, currYAngle, 0);

        // ī�޶� ��ġ Ÿ�� ȸ��������ŭ ȸ�� ��, dist��ŭ ����, height��ŭ ���� �ø���
        tr.position = target.position - (rot * Vector3.forward * dist) + (Vector3.up * height);
        

        // Ÿ�� �ٶ󺸰�
        tr.LookAt(target);
    }
}
