using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Photon.MonoBehaviour
{
    public PhotonView photonView;
    public Rigidbody2D rb;
    //public Rigidbody rb;
    public Animator anim;
    //public GameObject playerCamera;
    public SpriteRenderer sr;
    public Text PlayerNameText;

    public bool IsGrounded = false;
    public float MoveSpeed;
    public float JumpForce;

    private void Awake()
    {
        if (photonView.isMine)
        {
            PlayerNameText.text = PhotonNetwork.playerName;
        }

        else
        {
            PlayerNameText.text = photonView.owner.name;
            PlayerNameText.color = Color.cyan;
        }

    }

    private void Update()
    {
        if (photonView.isMine)
        {
            CheckInput();
        }
    }

    private void CheckInput()
    {
        var move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        transform.position += move * MoveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            photonView.RPC("FlipXTrue", PhotonTargets.AllBuffered);
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            photonView.RPC("FlipXFalse", PhotonTargets.AllBuffered);
        }

        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) || ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))))
        {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }
    }

    [PunRPC]
    private void FlipXTrue()
    {
        sr.flipX = true;
    }

    [PunRPC]
    private void FlipXFalse()
    {
        sr.flipX = false;
    }
}
