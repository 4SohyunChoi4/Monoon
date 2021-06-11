using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMove : MonoBehaviour
{
    private Transform Target; //눈송이
    public Transform Obs;

    private Transform tr; //카메라
    public Transform Obstruction;
    float zoomSpeed = 1f;

    public float dist = 20f; // 카메라와의 거리
    public float height = 5f; // 카메라의 높이
    public float smoothRotate = 5.0f;


    [Header("CameraTouchRotation")]
    
    Vector3 FirstPoint;
    Vector3 SecondPoint;
    public float TurnSpeed;
    public Transform charTarget;
    private Camera cameratarget;
    private Vector2 PrevPoint;
    public float moveSensitivityX = 1.0f;
    public float moveSensitivityY = 1.0f;

    [HideInInspector]
    public bool updateZoomSensitivity = true;
    public float orthoZoomSpeed = 0.5f;
    
    void Start()
    {
        Target = GameObject.Find("Noonsong (1)").transform;
        if(Target == null) Target = GameObject.Find("Noonsong (1) (clone)").transform;
        Obstruction = Obs;
        tr = GetComponent<Transform>();
        cameratarget = Camera.main;
        TurnSpeed = 2f;
    }
   private void Update() {
   
    }
    void LateUpdate() {
       CamControl();
       ViewObstructed();

    }
    void CamControl()
    {       
        //부드러운 회전을 위한 Mathf.LerpAngle
        float currYAngle = Mathf.LerpAngle(tr.eulerAngles.y, Target.eulerAngles.y, smoothRotate * Time.deltaTime);
        float currXAngle = Mathf.LerpAngle(tr.eulerAngles.x, Target.eulerAngles.x, smoothRotate * Time.deltaTime);
        //눈송이 뒤로 이동할 때(화면바라보고 이동할 때) 카메라 회전 제한
        if(MainPlayer.direction.y <= 0.1f)
          currYAngle = tr.eulerAngles.y;


        if(Input.GetAxis("Mouse ScrollWheel") < 0f)
            dist++;
        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
            dist--;


        if(Input.GetMouseButton(1))
        {
           currYAngle += -3 * Input.GetAxis("Mouse X") * smoothRotate;
            if(Input.GetAxis("Mouse Y") < 0f)
            height++;
            if(Input.GetAxis("Mouse Y") >0f)
            height--; 
        }
        
        height = Mathf.Clamp(height, 1, 15);
        dist = Mathf.Clamp(dist, 6,45);
        
        Quaternion rot = Quaternion.Euler(0, currYAngle, 0);

        tr.position = Target.position - (rot * Vector3.forward * dist) + (Vector3.up * height);
   
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
           
                if(Vector3.Distance(Obstruction.position, tr.position) >= 3f && Vector3.Distance(transform.position, Target.position) >= 1.5f ) //
                {
                    tr.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
                }
                
                
            }
            else
            {
                Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
             
                if(Vector3.Distance(tr.position, Obstruction.position) < 15.0f)
                {
                    transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime);
                }
                
            }
        }
    }
  

    void CameraTouchRotation()
    {

        #if UNITY_EDITOR
        
        Vector3 PositonInfo = tr.position - Target.position;
        PositonInfo = Vector3.Normalize(PositonInfo);

        tr.position = tr.position - (PositonInfo * Input.GetAxis("Mouse ScrollWheel") * TurnSpeed);
  
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