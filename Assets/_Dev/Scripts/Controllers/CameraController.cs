using System;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera swerveCamera;
    [SerializeField] private CinemachineVirtualCamera joystickCamera;

    private PlayerController _playerController;

    private void Awake()
    {
        GetReference();
        InitSubscribeEvents();
    }

    private void ChangeCamera()
    {
        swerveCamera.enabled = false;
        joystickCamera.enabled = true;
    }

    private void InitSubscribeEvents()
    {
        _playerController.OnCityEnter += ChangeCamera;
    }

    private void GetReference()
    {
        _playerController = PlayerController.Instance;
    }
}
