using UnityEngine;

public class ColorLine : MonoBehaviour, IInteractable
{
    private Material _lineMaterial;

    private void Awake()
    {
        InitValues();
    }

    public void Execute(PlayerController playerController)
    {
        playerController.isInRightLine = _lineMaterial.color == playerController.material.color;
    }
    
    private void InitValues()
    {
        _lineMaterial = GetComponent<MeshRenderer>().material;
    }
}
