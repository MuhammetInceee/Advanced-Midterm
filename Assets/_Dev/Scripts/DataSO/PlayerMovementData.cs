using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Player/MovementData", fileName = "Player Movement Data")]
public class PlayerMovementData : ScriptableObject
{
    [SerializeField, FoldoutGroup("SwerveMechanic")] private float VerticalSpeed;
    public float verticalSpeed => VerticalSpeed;
    
    [SerializeField, FoldoutGroup("SwerveMechanic")] private float HorizontalSpeed;
    public float horizontalSpeed => HorizontalSpeed;
    
    [SerializeField, FoldoutGroup("SwerveMechanic")] private float Sensitivity;
    public float sensitivity => Sensitivity;
}
