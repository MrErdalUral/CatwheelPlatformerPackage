using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CameraKinematicLerpMovement))]
[RequireComponent(typeof(CameraResizer))]
public class CameraController : MonoBehaviour
{

    private CameraResizer _cameraResizer;
    private CameraKinematicLerpMovement _cameraMovement;

    void Awake()
    {
        _cameraResizer = GetComponent<CameraResizer>();
        _cameraMovement = GetComponent<CameraKinematicLerpMovement>();
    }

    public void SetCamera(Level level)
    {
        //Start the movement of the camera towards the new level
        _cameraMovement.SetTargetPosition2D(level.transform.position);
        //Start the resizing of the orthographic size value of the camera to match the level size
        _cameraResizer.TargetSize = level.LevelCameraSize;
    }
}
