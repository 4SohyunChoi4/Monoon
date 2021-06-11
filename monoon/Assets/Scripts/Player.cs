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
                        {
                            SetDestination(hit.point);
                            /*if (collisionDetect != hit.collider.tag)
                            {
                                SetDestination(hit.point);
                                transform.position = hit.point;
                            }*/
                        }
                    }
                    if (hit.collider.gameObject == gameObject && !IsExpression) // 내 캐릭터를 클릭하고 표정창이 떠 있는 상태가 아니라면
                    {
                        Expression.SetActive(true);
                        IsExpression = true;
                    }
                    else if (hit.collider.gameObject == gameObject && IsExpression) // 내 캐릭터를 클릭하고 표정창이 떠 있는 상태라면
                    {
                        Expression.SetActive(false);
                        IsExpression = false;
                    }
                }
            }
            Move();
        }
        /*else // 다른 사람 캐릭터 클릭 시
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
        animator.SetBool("Surprise", false); // 모션 해제
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
            Debug.Log("의자 충돌!");
            animator.SetBool("isSit", true); //앉는 모션 출력
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


