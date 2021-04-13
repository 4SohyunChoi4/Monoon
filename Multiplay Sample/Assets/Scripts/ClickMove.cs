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
                // �̵� ����
                movePos = raycastHit.point;
            }
            Debug.DrawRay(ray.origin, ray.direction * 100.0f, Color.green);
        }

        if (movePos != Vector3.zero)
        {
            // ������ ���Ѵ�. 
            Vector3 dir = movePos - transform.position;

            // ������ �̿��� ȸ������ ���Ѵ�.
            float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

            // ȸ�� �� �̵� 
            transform.rotation = Quaternion.Euler(0, angle, 0);
            transform.position = Vector3.MoveTowards(transform.position, movePos, moveSpeed * Time.deltaTime);
        }
        // ������ġ�� ��ǥ��ġ ������ �Ÿ��� ���Ѵ�.
        float dis = Vector3.Distance(transform.position, movePos);

        // ��ǥ���� ���޽� �̵������� �ʱ�ȭ�� �߰����� �������� �����Ѵ�. 
        if (dis <= 0.3f)
        {
            movePos = Vector3.zero;
        }
    }
}