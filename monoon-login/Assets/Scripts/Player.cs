using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public PhotonView photonView;
    public Rigidbody rb;
    //public Animator anim;
    ///public GameObject PlayerCamera; // 플레이어별 카메라 지정
    public Text PlayerNameText;

    public bool IsGrounded = false;
    public float MoveSpeed;
    public float JumpForce;

    // 나경님 코드 추가 사항
    private Camera camera;
    private Animator animator;

    private bool isMove;
    private Vector3 destination;
    //

    private void Awake()
    {
        if (photonView.isMine)
        {
            //PlayerCamera.SetActive(true); // 플레이어별 카메라 지정
            PlayerNameText.text = PhotonNetwork.playerName;
            PlayerNameText.color = Color.cyan;
        }

        else
        {
            //Destroy(GetComponentInChildren<Camera>().gameObject); // 카메라가 플레이어별로 존재할 경우 메인 카메라 파괴를 위함
            PlayerNameText.text = photonView.owner.name;
            Destroy(rb);
        }
        
        // 나경님 코드
        camera = Camera.main;
        animator = GetComponent<Animator>();
        animator.applyRootMotion = true;
        
    }

    private void Update()
    {
        if (photonView.isMine)
        {
            // 나경님 코드
            if (Input.GetMouseButtonUp(0))
            {
                //Debug.Log("클릭 인식");
                RaycastHit hit;
                if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    if (!EventSystem.current.IsPointerOverGameObject()) // UI 터치 시 이동 방지
                    {
                        if (hit.collider.tag == "road") // road라는 tag를 가진 물체를 클릭해야 클릭 위치를 목적지로 인식
                            SetDestination(hit.point);
                    }

                }
            }
            Move();
        }
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
            transform.position += dir.normalized * Time.deltaTime * 5f;
        }

        if (Vector3.Distance(transform.position, destination) <= 0.1f)
        {
            isMove = false;
            animator.SetBool("isMove", false);
        }
    }

    /*private void CheckInput()
    {

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.down * MoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.up * MoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime);
        }



        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }
    }*/

    /*private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else if (stream.isReading)
        {
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();
        }
    }*/
}


