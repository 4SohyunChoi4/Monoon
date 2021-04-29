using UnityEngine;
using UnityEngine.EventSystems;

public class ClickMove : MonoBehaviour
{
    public Camera camera;
    private Animator animator;

    private bool isMove;
    private Vector3 destination;

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log("클릭 인식");
            RaycastHit hit;
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if(hit.collider.tag == "road")
                SetDestination(hit.point);
            }
        }
        Move();
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
            //animator.SetBool("isMove", false);
        }
    }
    private void SetDestination(Vector3 dest)
    {
        destination = dest;
        isMove = true;
        //animator.SetBool("isMove", true);
    }

}