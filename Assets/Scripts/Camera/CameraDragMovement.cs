using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDragMovement : MonoBehaviour
{
    
    [SerializeField]
    private CameraSettingsSO cameraSettings;
    [SerializeField]
    private MapInformationsSO mapInformations;
    private Vector3 startPos = Vector3.zero;
    private Vector3 endPos = Vector3.zero;
    private Vector3 direction = Vector3.zero;


    void FixedUpdate()
    {
        // Calculate the fraction of the total duration that has passed.
        if(Input.GetMouseButtonDown(0)) // check starting position of input
        {
            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);         
        }

        if(Input.touchCount == 2) // pinch to zoom
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroStart = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOneStart = touchOne.position - touchOne.deltaPosition;

            float startMagnitude = (touchZeroStart - touchOneStart).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - startMagnitude;

            Zoom(difference * cameraSettings._pinchZoomSpeed);
        }
        else if(Input.GetMouseButton(0)) // move around with both mouse and finger
        {
            direction = startPos - Camera.main.ScreenToWorldPoint(Input.mousePosition);   
            endPos = direction + transform.position;    
        }
 
        // We move only if it is needed and only inside desired boundaries
        if(endPos != Vector3.zero 
        && (endPos.x < mapInformations._dungeonWidth + cameraSettings._maxCameraOffsetX 
        && endPos.x > 0 - cameraSettings._maxCameraOffsetX) 
        && (endPos.y < mapInformations._dungeonHeight + cameraSettings._maxCameraOffsetY 
        && endPos.y > 0 - cameraSettings._maxCameraOffsetY))  
        {
            transform.position = Vector3.Lerp(transform.position, endPos, Time.fixedDeltaTime * cameraSettings._dragAcceleration);
        } 

        Zoom(Input.GetAxis("Mouse ScrollWheel") * cameraSettings._zoomSpeed);
    }


    void Zoom(float amount)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - amount, cameraSettings._zoomOutMin, cameraSettings._zoomOutMax);
    }
}
