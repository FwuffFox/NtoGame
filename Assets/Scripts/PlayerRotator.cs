using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
    private Camera _mainCamera;

    private void OnEnable()
    {
        _mainCamera = Camera.main;
    }

    private void FixedUpdate () 
    {
        LookAtCamera();
    }
    
    private void LookAtCamera()
    {
        var cameraRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
        
        if (!Physics.Raycast(cameraRay, out var cameraRayHit) || !cameraRayHit.transform.CompareTag("Ground")) return;
        
        var targetPosition = new Vector3(cameraRayHit.point.x, transform.position.y, cameraRayHit.point.z);
        transform.LookAt(targetPosition);
    }
}
