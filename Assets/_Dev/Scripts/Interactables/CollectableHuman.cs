using System.Linq;
using UnityEngine;

public class CollectableHuman : MonoBehaviour, IInteractable
{
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
            Transform targetHolder = TargetHolderTransform(playerController);
            transform.parent = targetHolder;
            transform.localPosition = new Vector3(0, -0.645f,0);
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
    
    private Transform TargetHolderTransform(PlayerController playerController)
    {
        Transform target = playerController.holdersTr.FirstOrDefault(m => m.transform.childCount == 1);
        if (target != null)
        {
            return target!.transform;
        }
        return playerController.holdersTr[^1].transform;
    }
    
    private void InitVariables()
    {
        // _materialName = GetComponent<MeshRenderer>().material.name;
        _collider = GetComponent<Collider>();
    }


}
