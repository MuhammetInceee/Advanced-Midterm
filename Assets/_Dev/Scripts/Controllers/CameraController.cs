using Cinemachine;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    [SerializeField] private GameObject swerveCamera;
    [SerializeField] private GameObject joystickCamera;

    private PlayerController _playerController;

    public void Initialize()
    {
        GetReference();
        InitSubscribeEvents();
    }

    private void ChangeCamera()
    {
        swerveCamera.SetActive(false);
        joystickCamera.SetActive(true);
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
