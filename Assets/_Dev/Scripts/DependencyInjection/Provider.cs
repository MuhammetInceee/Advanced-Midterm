using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Provider : MonoBehaviour, IDependencyProvider
{
    [Provide]
    public CameraController ProvideCameraController()
    {
        return CameraController.Instance;
    }
    
    [Provide]
    public PlayerController ProvidePlayerController()
    {
        return PlayerController.Instance;
    }
    
    [Provide]
    public TurretController ProvideTurretController()
    {
        return TurretController.Instance;
    }

    [Provide]
    public UIController ProvideUIController()
    {
        return UIController.Instance;
    }
}
