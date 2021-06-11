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

    //나경님 코드 추가 사항
    private Camera camera;
    private Animator animator;

    private bool isMove;
    private Vector3 destination;
    public static Vector3 direction;
    GameObject nearObject;

    void OnLevelWasLoaded(int sceneIndex)
    {
        Debug.Log(sceneIndex);
        if( sceneIndex == 1) CameraMain();
    }	


    private void Awake()
    {
        //나경님 코드
        camera = Camera.main;
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
            //Debug.Log("클릭!");
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
            {
            
                if (!EventSystem.current.IsPointerOverGameObject()) // UI 터치 시 이동 방지
                    if (hit.collider.tag == "road") //road라는 tag를 가진 물체를 클릭해야 클릭 위치를 목적지로 인식
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

      //캐릭터 밀림 방지
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
        //Debug.Log("충돌!");
    }
}


