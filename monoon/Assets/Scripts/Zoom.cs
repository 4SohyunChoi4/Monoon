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

            //Ű����� �Է��� �� (�� ���ư����� �׽�Ʈ �뵵��)
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
