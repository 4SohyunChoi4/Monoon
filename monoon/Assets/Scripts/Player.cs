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
    private Vector3 direction;
    //
    public GameObject Expression;
    private bool IsExpression;

    Button Hi;
    Button Surprise;
    Button Sit;

    public GameObject Whisper;

    string collisionDetect;

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
        IsExpression = false;

        //rb = GetComponent<Rigidbody>();

        Hi = transform.Find("Canvas").transform.Find("Expression").transform.Find("HiButton").GetComponent<Button>();
        Surprise = transform.Find("Canvas").transform.Find("Expression").transform.Find("SurpriseButton").GetComponent<Button>();
        Sit = transform.Find("Canvas").transform.Find("Expression").transform.Find("SitButton").GetComponent<Button>();

        Hi.onClick.AddListener(ClickHiButton);
        Surprise.onClick.AddListener(ClickSurpriseButton);
        Sit.onClick.AddListener(ClickSitButton);

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
                        {
                            SetDestination(hit.point);
                            /*if (collisionDetect != hit.collider.tag)
                            {
                                SetDestination(hit.point);
                                transform.position = hit.point;
                            }*/
                        }
                    }
                    if (hit.collider.gameObject == gameObject && !IsExpression) // �� ĳ���͸� Ŭ���ϰ� ǥ��â�� �� �ִ� ���°� �ƴ϶��
                    {
                        Expression.SetActive(true);
                        IsExpression = true;
                    }
                    else if (hit.collider.gameObject == gameObject && IsExpression) // �� ĳ���͸� Ŭ���ϰ� ǥ��â�� �� �ִ� ���¶��
                    {
                        Expression.SetActive(false);
                        IsExpression = false;
                    }
                }
            }
            Move();
        }
        /*else // �ٸ� ��� ĳ���� Ŭ�� ��
        {
            if (Input.GetMouseButtonUp(0))
            {
                RaycastHit hit;
                if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    if (hit.collider.tag == "Player")
                    {
                        Whisper.SetActive(true);
                    }
                }
            }
        }*/
    }

    private void SetDestination(Vector3 dest)
    {
        destination = dest;
        animator.SetBool("isSit", false);
        animator.SetBool("Hi", false);
        animator.SetBool("Surprise", false); // ��� ����
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
            direction = dir;
        }

        if (Vector3.Distance(transform.position, destination) <= 0.1f)
        {
            isMove = false;
            animator.SetBool("isMove", false);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        collisionDetect = collision.gameObject.tag;
        if (collision.gameObject.tag == "sofa")
        {
            Debug.Log("���� �浹!");
            animator.SetBool("isSit", true); //�ɴ� ��� ���
            isMove = false;
        }
    }

    public void ClickHiButton()
    {
        animator.SetBool("Hi", true);
        isMove = false;
        Expression.SetActive(false);
    }
    public void ClickSurpriseButton()
    {
        animator.SetBool("Surprise", true);
        isMove = false;
        Expression.SetActive(false);
    }
    public void ClickSitButton()
    {
        animator.SetBool("isSit", true);
        isMove = false;
        Expression.SetActive(false);
    }
}


