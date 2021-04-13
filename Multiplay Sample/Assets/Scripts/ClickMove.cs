using UnityEngine;
using UnityEngine.EventSystems;

public class ClickMove : MonoBehaviour
{
    public float moveSpeed = 10.0f;

    private Vector3 movePos = Vector3.zero;
    private Vector3 moveDir = Vector3.zero;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // RaycastHit raycastHit;
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                // 이동 지점
                movePos = raycastHit.point;
            }
            Debug.DrawRay(ray.origin, ray.direction * 100.0f, Color.green);
        }

        if (movePos != Vector3.zero)
        {
            // 방향을 구한다. 
            Vector3 dir = movePos - transform.position;

            // 방향을 이용해 회전각을 구한다.
            float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

            // 회전 및 이동 
            transform.rotation = Quaternion.Euler(0, angle, 0);
            transform.position = Vector3.MoveTowards(transform.position, movePos, moveSpeed * Time.deltaTime);
        }
        // 현재위치와 목표위치 사이의 거리를 구한다.
        float dis = Vector3.Distance(transform.position, movePos);

        // 목표지점 도달시 이동지점을 초기화해 추가적인 움직임을 제한한다. 
        if (dis <= 0.3f)
        {
            movePos = Vector3.zero;
        }
    }
}