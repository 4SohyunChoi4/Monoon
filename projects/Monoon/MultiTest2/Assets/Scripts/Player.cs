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
    ///public GameObject PlayerCamera; // �÷��̾ ī�޶� ����
    public Text PlayerNameText;

    public bool IsGrounded = false;
    public float MoveSpeed;
    public float JumpForce;

    // ����� �ڵ� �߰� ����
    private Camera camera;
    private Animator animator;

    private bool isMove;
    private Vector3 destination;
    //

    private void Awake()
    {
        if (photonView.isMine)
        {
            //PlayerCamera.SetActive(true); // �÷��̾ ī�޶� ����
            PlayerNameText.text = PhotonNetwork.playerName;
            PlayerNameText.color = Color.cyan;
        }

        else
        {
            //Destroy(GetComponentInChildren<Camera>().gameObject); // ī�޶� �÷��̾�� ������ ��� ���� ī�޶� �ı��� ����
            PlayerNameText.text = photonView.owner.name;
            Destroy(rb);
        }
        
        // ����� �ڵ�
        camera = Camera.main;
        animator = GetComponent<Animator>();
        animator.applyRootMotion = true;
        
    }

    private void Update()
    {
        if (photonView.isMine)
        {
            // ����� �ڵ�
            if (Input.GetMouseButtonUp(0))
            {
                //Debug.Log("Ŭ�� �ν�");
                RaycastHit hit;
                if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    if (!EventSystem.current.IsPointerOverGameObject()) // UI ��ġ �� �̵� ����
                    {
                        if (hit.collider.tag == "road") // road��� tag�� ���� ��ü�� Ŭ���ؾ� Ŭ�� ��ġ�� �������� �ν�
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


