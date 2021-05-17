using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    private Camera camera;
    private float startingFOV;

    public float minFOV = 10f;
    public float maxFOV= 100f;
    public float zoomRate = 0.5f;

    private float currentFOV;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        startingFOV = camera.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        currentFOV = camera.fieldOfView;

            //키보드로 입력할 때 (잘 돌아가는지 테스트 용도로)
        if (Input.GetKey(KeyCode.P))
        {
            currentFOV = startingFOV;
        }

        if(Input.GetKey(KeyCode.I))
        {
            currentFOV -= zoomRate;
        }

        if (Input.GetKey(KeyCode.O))
        {
            currentFOV += zoomRate;
        }

        currentFOV = Mathf.Clamp(currentFOV, minFOV, maxFOV);
        camera.fieldOfView = currentFOV;

    }
}
