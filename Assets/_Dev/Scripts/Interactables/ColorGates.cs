using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class ColorGates : MonoBehaviour, IInteractable
{
    [SerializeField, EnumToggleButtons] GateColorTypes Type;
    private GateColorType type => new (Type);
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        GetReference();
        InitVariables();
    }

    public void Execute(PlayerController playerController)
    {
        foreach (GameObject obj in playerController.stackList)
        {
            obj.GetComponentInChildren<SkinnedMeshRenderer>().material.color = type.GateColor;
        }
    }
    
    private void GetReference()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    
    private void InitVariables()
    {
        _meshRenderer.material.color = type.GateColor;
        _meshRenderer.material.DOFade(10, 0);
    }
}

public enum GateColorTypes{Red, Green, Blue}

[Serializable]
public class GateColorType
{
    internal static readonly GateColorType Red = new(GateColorTypes.Red);
    internal static readonly GateColorType Green = new(GateColorTypes.Green);
    internal static readonly GateColorType Blue = new(GateColorTypes.Blue);

    private readonly GateColorTypes gateColorTypes;
    internal GateColorType(GateColorTypes gateColorTypes) => this.gateColorTypes = gateColorTypes;
        
    internal Color GateColor => gateColorTypes switch
    {
        GateColorTypes.Red => Color.red,
        GateColorTypes.Green => Color.green,
        GateColorTypes.Blue => Color.blue,
        _ => throw new ArgumentOutOfRangeException()
    };
}
