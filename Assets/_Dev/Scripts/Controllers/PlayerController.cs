using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public event Action OnPlay;
    public Action OnCityEnter;
    
    public List<GameObject> stackList = new();
    public List<Transform> holdersTr;
    public GameObject rope;

    internal Material material;
    internal bool isInRightLine = true;
    
    [SerializeField] private FloatingJoystick joystick;
    
    private Rigidbody _rb;
    private PlayerMovementData _movementData;
    private UIController _controllerUI;

    [Header("TempData")] 
    internal float tempVerticalSpeed;
    private float _tempHorizontalSpeed;
    
    private void Awake()
    {
        GetReference();
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
        _rb.velocity = new Vector3(Mathf.Clamp(_tempHorizontalSpeed, -10, 10), _rb.velocity.y,
            tempVerticalSpeed);
    }

    private void SwerveHorizontalMovement()
    {
        if (Input.GetMouseButton(0))
            _tempHorizontalSpeed = Input.GetAxis("Mouse X") * _movementData.sensitivity;

        else _tempHorizontalSpeed = 0;
    }

    internal void SwerveInput()
    {
        SwerveVerticalMovement();
        SwerveHorizontalMovement();
    }

    internal void JoyStickMovement()
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

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            interactable.ExecuteExit(this);
        }
    }

    #endregion

    private void CityEnter()
    {
        _rb.velocity = Vector3.zero;
        OnPlay -= SwerveInput;
        OnPlay += JoyStickMovement;
    }

    private void GetReference()
    {
        _rb = GetComponent<Rigidbody>();
        _movementData = Resources.Load<PlayerMovementData>("Data/Player/PlayerMovementData");
        material = GetComponentInChildren<SkinnedMeshRenderer>().material;
        _controllerUI = UIController.Instance;
    }
    
    private void InitValues()
    {
        _tempHorizontalSpeed = _movementData.horizontalSpeed;
        tempVerticalSpeed = _movementData.verticalSpeed;
        stackList.Add(gameObject);
    }
    
    private void InitSubscribeEvents()
    {
        OnCityEnter += CityEnter;
        _controllerUI.OnLevelFail += DeinitializeInput;
    }

    public void InitInput()
    {
        OnPlay += SwerveInput;
    }

    private void DeinitializeInput()
    {
        OnPlay -= SwerveInput;
    }
}
