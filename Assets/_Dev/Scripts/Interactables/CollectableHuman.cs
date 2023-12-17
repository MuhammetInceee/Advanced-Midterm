using UnityEngine;

public class CollectableHuman : MonoBehaviour, IInteractable
{
    private const float StackOffset = -1.4f;
    
    private string _materialName;
    private Collider _collider;

    private void Awake()
    {
        InitVariables();
    }
    
    public void Execute(PlayerController playerController)
    {
        var list = playerController.stackList;
        
        // if (playerController.materialName == _materialName)
        // {
            //Collect
            list.Add(gameObject);
            transform.parent = playerController.transform;
            transform.localPosition = new Vector3(0, 0, list.IndexOf(gameObject) * StackOffset);
            _collider.enabled = false;
        // }
        // else
        // {
        //     //Destroy Last Element
        //     GameObject targetObj = list[^1];
        //
        //     list.Remove(targetObj);
        //     Destroy(targetObj);
        //     Destroy(gameObject);
        //
        //     if (list.Count == 0)
        //     {
        //         print($"Game Over");
        //         //TODO Game Over Action will Invoked
        //     }
        // }
    }
    
    private void InitVariables()
    {
        _materialName = GetComponent<MeshRenderer>().material.name;
        _collider = GetComponent<Collider>();
    }
}
