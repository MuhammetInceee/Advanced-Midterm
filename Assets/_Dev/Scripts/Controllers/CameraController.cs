using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject swerveCamera;
    [SerializeField] private GameObject joystickCamera;

    private PlayerController _playerController;

    private void Awake()
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
