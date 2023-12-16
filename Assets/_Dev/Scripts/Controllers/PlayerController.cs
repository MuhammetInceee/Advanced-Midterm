using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameController _gameController;
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

    private void InitVariables()
    {
        _gameController = GameController.Instance;
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
        _gameController.PlayerInput += SwerveHorizontalMovement;
        _gameController.PlayerInput += SwerveVerticalMovement;
    }
    
    //TODO Player'ı durdurduğun zaman velocity sıfırla
}
