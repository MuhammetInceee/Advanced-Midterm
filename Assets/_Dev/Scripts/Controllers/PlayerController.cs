using System;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public event Action OnPlay;
    public Action OnCityEnter;
    
    [SerializeField] private FloatingJoystick joystick;
    
    private Rigidbody _rb;
    private PlayerMovementData _movementData;

    [Header("TempData")] 
    private float _verticalSpeed;
    private float _horizontalSpeed;
    
    private void Awake()
    {
        InitVariables();
        InitValues();
        InitSubscribeEvents();
    }

    private void Update()
    {
        OnPlay?.Invoke();
    }

    #region PlayerMovement

    private void SwerveVerticalMovement()
    {
        _rb.velocity = new Vector3(Mathf.Clamp(_horizontalSpeed, -10, 10), _rb.velocity.y,
            _verticalSpeed);
    }

    private void SwerveHorizontalMovement()
    {
        if (Input.GetMouseButton(0))
            _horizontalSpeed = Input.GetAxis("Mouse X") * _movementData.sensitivity;

        else _horizontalSpeed = 0;
    }

    private void JoyStickMovement()
    {
        if (joystick.Direction.magnitude > 0.05f)
        {
            var speed = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
            

            _rb.velocity = new Vector3(joystick.Horizontal * _movementData.movementSpeed, _rb.velocity.y,
                joystick.Vertical * _movementData.movementSpeed);

            if (joystick.Horizontal != 0 || joystick.Vertical != 0)
            {
                transform.rotation = Quaternion.LookRotation(_rb.velocity);
            }
            
        }
    }
    
    #endregion

    #region PlayerCollision

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            interactable.Execute(this);
        }
    }

    #endregion

    private void InitVariables()
    {
        _rb = GetComponent<Rigidbody>();
        _movementData = Resources.Load<PlayerMovementData>("Data/Player/PlayerMovementData");
    }
    
    private void InitValues()
    {
        _horizontalSpeed = _movementData.horizontalSpeed;
        _verticalSpeed = _movementData.verticalSpeed;
    }
    
    private void InitSubscribeEvents()
    {
        OnPlay += JoyStickMovement;
    }

    //TODO Player'ı durdurduğun zaman velocity sıfırla
}
