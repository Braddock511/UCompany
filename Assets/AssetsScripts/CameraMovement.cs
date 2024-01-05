using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Unity
    public Camera cam;
    // Variables
    private Vector3 dragOrigin;
    public SpriteRenderer mapRenderer;
    private float mapMinX, mapMaxX, mapMinY, mapMaxY;
    public float zoomSpeed = 1.2f;
    public float maxZoom = 5f; 
    public bool isActive = true;
    
    private void Awake()
    {
        mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x / 2f;
        mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2f;

        mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y / 2f;
        mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y / 2f;
    }

    void Update()
    {
        if(isActive)
        {
            MoveCamera();
            Zoom();
        }
    }

    private void Zoom()
    {
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");

        if (scrollDelta != 0f)
        {
            float newZoom = cam.orthographicSize - scrollDelta * zoomSpeed;
    
            newZoom = Mathf.Clamp(newZoom, 2.5f, maxZoom);
            cam.orthographicSize = newZoom;
            cam.transform.position = ClampCam(cam.transform.position);
        }
    }

    private void MoveCamera()
    {
        if(Input.GetMouseButtonDown(0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if(Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

            cam.transform.position = ClampCam(cam.transform.position + difference);
        }
    }

    private Vector3 ClampCam(Vector3 targetPosition)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float nweY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, nweY, targetPosition.z);
    }
}
