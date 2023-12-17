using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    private PlayerController _playerController;

    [SerializeField] private Transform shootPoint;

    private void Awake()
    {
        InitVariables();
    }

    internal void Fire()
    {
        
    }
    
    private void InitVariables()
    {
        _playerController = PlayerController.Instance;
    }
}
