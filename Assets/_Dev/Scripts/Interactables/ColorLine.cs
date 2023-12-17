using UnityEngine;

public class ColorLine : MonoBehaviour, IInteractable
{
    [SerializeField] private Material lineMaterial;
    
    public void Execute(PlayerController playerController)
    {
        playerController.isInRightLine = lineMaterial.color == playerController.material.color;
    }
}
