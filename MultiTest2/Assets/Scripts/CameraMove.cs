using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMove : MonoBehaviour
{
    private Transform Target, Obs; //������

    private Transform tr; //ī�޶�
    public Transform Obstruction;
    float zoomSpeed = 1f;

    public float dist = 20f; // ī�޶���� �Ÿ�
    public float height = 5f; // ī�޶��� ����
    public float smoothRotate = 5.0f;


    [Header("CameraTouchRotation")]
    
    Vector3 FirstPoint;
    Vector3 SecondPoint;
    public float TurnSpeed;
    public float CamPos;
    public Transform charTarget;
    private Camera cameratarget;
    private Vector2 PrevPoint;
    public float moveSensitivityX = 1.0f;
    public float moveSensitivityY = 1.0f;
    float xAngle;
    float yAngle;
    float xAngleTemp;
    float yAngleTemp;
    public float m_DoubleClickSecond = 0.2f;
    private bool m_IsOneClick = false;
    private double m_Timer = 0;

    [HideInInspector]
    public bool isCanRotate = true;
    private bool isMouseDown = false;

    public bool updateZoomSensitivity = true;
    public float orthoZoomSpeed = 0.5f;
    public float minZoom = 1.0f;
    public float maxZoom = 20.0f;
    public float perspectiveZoomSpeed = .5f;
    


    void Start()
    {
        Target = GameObject.Find("Noonsong (1)").transform;
        if(Target == null) Target = GameObject.Find("Noonsong (1) (clone)").transform;
        Obstruction = Obs;
        tr = GetComponent<Transform>();
        cameratarget = Camera.main;
        if (cameratarget == null) Debug.Log("camera target is null");
        xAngle = 0;
        yAngle = 50;
        transform.rotation = Quaternion.Euler(yAngle, xAngle, 0);
        TurnSpeed = 2f;
        CamPos = 2f;
        //dollyDir = transform.localPosition.normalized;   //�θ��� ������� �Ÿ�
        
        //dollyDir = transform.position.normalized;
       // distance = transform.position.magnitude; //���ͱ���
    }
   private void Update() {
       /*
       if (m_IsOneClick && ((Time.time - m_Timer) > m_DoubleClickSecond))
       {
           Debug.Log("One Click");
           m_IsOneClick = false;
       }

       if(Input.GetMouseButtonDown(0))
       {
           if(!m_IsOneClick)
           {
               m_Timer = Time.time;
               m_IsOneClick = true;
           }
           else if (m_IsOneClick && ((Time.time - m_Timer) < m_DoubleClickSecond))
           {
               Debug.Log("Double Click");
               m_IsOneClick = false;
           }
           
       }
        
        //Vector3 PositonInfo = tr.position - Target.position;
        //PositonInfo = Vector3.Normalize(PositonInfo);

       // tr.position = tr.position - (Vector3.Normalize(tr.position - Target.position) * Input.GetAxis("Mouse ScrollWheel") * TurnSpeed);
       */
    }
    void LateUpdate() {
       CamControl();
       ViewObstructed();
     //  Cameramove2();
    }
    void CamControl()
    {       
        //�ε巯�� ȸ���� ���� Mathf.LerpAngle
        float currYAngle = Mathf.LerpAngle(tr.eulerAngles.y, Target.eulerAngles.y, smoothRotate * Time.deltaTime);
        float currXAngle = Mathf.LerpAngle(tr.eulerAngles.x, Target.eulerAngles.x, smoothRotate * Time.deltaTime);
        //������ �ڷ� �̵��� ��(ȭ��ٶ󺸰� �̵��� ��) ī�޶� ȸ�� ����
        if(MainPlayer.direction.y <= 0.1f)
          currYAngle = tr.eulerAngles.y;

        
        // ���Ϸ� Ÿ���� ���ʹϾ����� �ٲٱ�(�̵��� �ޱ��� ȸ������ ��ȯ)
        //Quaternion rot = Quaternion.Euler(0, currYAngle, 0);

        if(Input.GetAxis("Mouse ScrollWheel") < 0f)
            dist++;
        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
            dist--;


        //Vector3 rot = tr.rotation.eulerAngles;
        if(Input.GetMouseButton(1))
        {
            //currXAngle += 3 *Input.GetAxis("Mouse Y") * smoothRotate;
           currYAngle += -3 * Input.GetAxis("Mouse X") * smoothRotate;
            if(Input.GetAxis("Mouse Y") < 0f)
            height++;
            if(Input.GetAxis("Mouse Y") >0f)
            height--; 
        }

        

        
        height = Mathf.Clamp(height, 1, 15);
        dist = Mathf.Clamp(dist, 6,45);
        //Quaternion rot = Quaternion.Euler(0, currYAngle, 0);
        
        Quaternion rot = Quaternion.Euler(0, currYAngle, 0);
       //{
         //   dist = 15f;
           // height = 5f;
        //}
//        Event e = Event.current;
/*
        if(m_IsOneClick == false)
        {
            Debug.Log("DoubleClick");
            dist = 15f;
            height = 5f;
        }
    */
        tr.position = Target.position - (rot * Vector3.forward * dist) + (Vector3.up * height);
        //transform.RotateAround(Target.position, Vector3.right, Input.GetAxis("Mouse Y") * TurnSpeed);
        // ī�޶� ��ġ Ÿ�� ȸ��������ŭ ȸ�� ��, dist��ŭ ����, height��ŭ ���� �ø���


        

        // Ÿ�� �ٶ󺸰�
        tr.LookAt(Target);
    }


    void ViewObstructed()
    {
        RaycastHit hit;
      
        if(Physics.Raycast(tr.position, Target.position - tr.position, out hit, 15.0f))
       
        {
            if(hit.collider.gameObject.tag == "building")
            {
                
                Obstruction = hit.transform;
            
               
                Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                //Obstruction.gameObject.GetComponent<MeshCollider>().enabled =false;
               
              
               
             
              if(Vector3.Distance(Obstruction.position, tr.position) >= 3f && Vector3.Distance(transform.position, Target.position) >= 1.5f ) //
                {
                    tr.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
                }
                
                
            }
            else
            {
                Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                //Obstruction.gameObject.GetComponent<MeshCollider>().enabled =true;
                //Debug.Log("����-����Ÿ�" + distance);

                if(Vector3.Distance(tr.position, Obstruction.position) < 15.0f)
                {
                    transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime);
                }
                
            }
        }
    }
    
    //*/
    /*
    void Cameramove2()
    {
   //dollyDir = transform.localPosition.normalized;   //�θ��� ������� �Ÿ�
        //dollyDir = transform.position.normalized;
        //distance = transform.position.magnitude; //���ͱ���


        Vector3 desiredCameraPos = transform.TransformPoint(dollyDir * maxDistance);
        RaycastHit hit;

        if (Physics.Linecast (transform.position, Target.position - tr.position, out hit,10.0f))
        {
            distance = Mathf.Clamp ((hit.distance * dis_ray), minDistance, maxDistance);

        }
        else
        {
            distance = maxDistance;
        }
        transform.position = Vector3.Lerp(transform.position, dollyDir * distance, Time.deltaTime * smooth);

    }
    */

    
    

    void CameraTouchRotation()
    {

        #if UNITY_EDITOR
        
        Vector3 PositonInfo = tr.position - Target.position;
        PositonInfo = Vector3.Normalize(PositonInfo);

        tr.position = tr.position - (PositonInfo * Input.GetAxis("Mouse ScrollWheel") * TurnSpeed);
        /*
        if(Input.GetMouseButtonDown(1))
        {
            FirstPoint = Input.mousePosition;
            xAngleTemp = xAngle;
            yAngleTemp = yAngle;
            isMouseDown = true;
        }

        if(Input.GetMouseButtonUp(1))
        {
            SecondPoint = Input.mousePosition;
            xAngle = xAngleTemp + (SecondPoint.x - FirstPoint.x) * 180 /Screen.width;
            xAngle = yAngleTemp - (SecondPoint.y - FirstPoint.y) * 90 * 3f / Screen.height;

            if (yAngle < 40f)
                yAngle = 40f;
            if (yAngle > 85f)
                yAngle = 85f;

            transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
        }
*/

        #endif

        Vector3 PositionInfo = tr.position - Target.position;
        PositionInfo = Vector3.Normalize(PositionInfo);

        if(updateZoomSensitivity)
        {
            moveSensitivityX = cameratarget.orthographicSize / 5.0f;
            moveSensitivityY = cameratarget.orthographicSize / 5.0f;
        }

        Touch[] touches = Input.touches;

        if(cameratarget)
        {
            if(Input.touchCount == 1)
            {
                if(Input.GetTouch(0).phase == TouchPhase.Moved) {
                    PrevPoint = Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition;

                    Target.transform.Rotate (0, -(Input.GetTouch(0).position.x - PrevPoint.x), 0);
                    cameratarget.transform.RotateAround (Target.position, Vector3.right, - (Input.GetTouch(0).position.y -  PrevPoint.y)*0.5f);

                    PrevPoint = Input.GetTouch(0).position;
                }
            }
        }

        if (cameratarget)
        {
            if(Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPervPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevTouchDeltaMag = (touchZeroPervPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                float deltaMagnitudediff = prevTouchDeltaMag - touchDeltaMag;

                tr.position = tr.position - -(PositionInfo * deltaMagnitudediff * orthoZoomSpeed);
            }
        }


        






           
    }

}