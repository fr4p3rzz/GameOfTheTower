using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "GameSettings/CameraSettings")]

public class CameraSettingsSO : ScriptableObject
{
    public float _zoomOutMin { get { return zoomOutMin; } private set { zoomOutMin = value; } }
    public float _zoomOutMax { get { return zoomOutMax; } set { zoomOutMax = value; } }
    public float _zoomSpeed { get { return zoomSpeed; } private set { zoomSpeed = value; } }
    public float _pinchZoomSpeed { get { return pinchZoomSpeed; } private set { pinchZoomSpeed = value; } }
    public float _dragAcceleration { get { return dragAcceleration; } private set { dragAcceleration = value; } }
    public float _maxCameraOffsetX { get { return maxCameraOffsetX; } private set { maxCameraOffsetX = value; } }
    public float _maxCameraOffsetY { get { return maxCameraOffsetY; } private set { maxCameraOffsetY = value; } }


    [SerializeField]
    private float zoomOutMin = 0.3f;
    [SerializeField]
    private float zoomOutMax = 4f;
    [SerializeField]
    [Range(0.1f, 25f)]
    private float zoomSpeed = 0.5f;
    [SerializeField]
    [Range(0.1f, 25f)]
    private float pinchZoomSpeed = 0.5f;
    [SerializeField]
    [Range(1f, 9f)]
    private float dragAcceleration = 2f;
    [SerializeField]
    [Range(0f, 25f)]
    private float maxCameraOffsetX = 50f;
    [SerializeField]
    [Range(0f, 25f)]
    private float maxCameraOffsetY = 50f;



}
