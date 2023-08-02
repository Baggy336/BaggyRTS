using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraTransform;

    public float cameraSpeed;
    public float moveTime;
    public float rotAmount;
    public float edgeSize = 10f;

    public Vector3 zoomAmount;
    public Vector3 newZoom;
    public Vector3 newPos;

    public Quaternion newRot;

    private void Start()
    {
        newPos = transform.position;
        newRot = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    private void Update()
    {
        HandleMovement();
        HandleZoom();
    }

    void HandleMovement()
    {
        if(Input.mousePosition.x > Screen.width - edgeSize)
        {
            //Right
            newPos += (transform.right * cameraSpeed);
        }
        if (Input.mousePosition.x < edgeSize)
        {
            //Left
            newPos += (transform.right * -cameraSpeed);
        }
        if (Input.mousePosition.y > Screen.height - edgeSize)
        {
            //Up
            newPos += (transform.forward * cameraSpeed);
        }
        if (Input.mousePosition.y < edgeSize)
        {
            //Down
            newPos += (transform.forward * -cameraSpeed);
        }
        
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * moveTime);
    }

    void HandleZoom()
    {
        if(Input.mouseScrollDelta.y != 0)
        {
            newZoom += Input.mouseScrollDelta.y * zoomAmount;
        }
        newZoom.y = Mathf.Clamp(newZoom.y, 40f, 250f);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * moveTime);
    }
}
