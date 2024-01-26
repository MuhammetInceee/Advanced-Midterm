using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    [Inject] CameraController cameraController;
    [Inject] PlayerController playerController;
    [Inject] TurretController turretController;
    [Inject] UIController uiController;
    
    private void Awake()
    {
        cameraController.Initialize();
        playerController.Initialize();
        turretController.Initialize();
        uiController.Initialize();
    }
}
