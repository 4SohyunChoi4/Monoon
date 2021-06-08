using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class MainPlayer : MonoBehaviour
{
    public Rigidbody rb;

    public bool IsGrounded = false;

    //����� �ڵ� �߰� ����
    private Camera camera;
    private Animator animator;

    private bool isMove;
    private Vector3 destination;
    public static Vector3 direction;

    public static int noondung = 100;

    void OnLevelWasLoaded(int sceneIndex)
    {
        Debug.Log(sceneIndex);
        if( sceneIndex == 1) CameraMain();
    }	
    private void Awake()
    {
        //����� �ڵ�
        //camera = Camera.main;
        CameraMain();
        animator = GetComponent<Animator>();
        animator.applyRootMotion = true;
    }

    public void CameraMain(){
        camera = Camera.main;
        Debug.Log("cameraMain");
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            //Debug.Log("Ŭ��!");
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
            {
            
                if (!EventSystem.current.IsPointerOverGameObject()) // UI ��ġ �� �̵� ����
                    if (hit.collider.tag == "road") //road��� tag�� ���� ��ü�� Ŭ���ؾ� Ŭ�� ��ġ�� �������� �ν�
                    { SetDestination(hit.point); }
            }
        }
        Move();
    }

    private void SetDestination(Vector3 dest)
    {
        destination = dest;
        isMove = true;
        animator.SetBool("isMove", true);
    }

    private void Move()
    {
        if (isMove)
        {
            var dir = destination - transform.position;
            transform.forward = dir;
            transform.position += dir.normalized * Time.deltaTime * 10f;
            direction = dir;
        }

        if (Vector3.Distance(transform.position, destination) <= 0.1f)
        {
            isMove = false;
            animator.SetBool("isMove", false);
        }
    }

      //ĳ���� �и� ����
    void FreezeRotation()
    {
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
    void FixedUpdate()
    {
        FreezeRotation();
    }
    public void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("�浹!");
    }

 
}


