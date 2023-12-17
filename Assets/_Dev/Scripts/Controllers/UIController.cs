using UnityEngine;

public class UIController : MonoBehaviour
{
    private PlayerController _playerController;
    
    [SerializeField] private GameObject joystick;

    private void Awake()
    {
        GetReferences();
        InitSubscribeEvent();
    }

    private void JoystickEnabled()
    {
        joystick.SetActive(true);
    }

    private void InitSubscribeEvent()
    {
        _playerController.OnCityEnter += JoystickEnabled;
    }

    private void GetReferences()
    {
        _playerController = PlayerController.Instance;
    }
}
