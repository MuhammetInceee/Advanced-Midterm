using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Player/MovementData", fileName = "Player Movement Data")]
public class PlayerMovementData : ScriptableObject
{
    [SerializeField] private float VerticalSpeed;
    public float verticalSpeed => VerticalSpeed;
    
    [SerializeField] private float HorizontalSpeed;
    public float horizontalSpeed => HorizontalSpeed;
    
    [SerializeField] private float Sensitivity;
    public float sensitivity => Sensitivity;
}
