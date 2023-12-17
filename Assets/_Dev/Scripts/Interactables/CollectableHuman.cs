using System.Linq;
using UnityEngine;

public class CollectableHuman : MonoBehaviour, IInteractable
{
    private static readonly int Run = Animator.StringToHash("Run");
    
    private Material _material;
    private Collider _collider;
    private UIController _controllerUI;
    private Animator _animator;

    private void Awake()
    {
        GetReference();
    }
    
    public void Execute(PlayerController playerController)
    {
        var list = playerController.stackList;
        
        if (playerController.material.color == _material.color)
        {
            Transform targetHolder = TargetHolderTransform(playerController);
            
            list.Add(gameObject);
            transform.parent = targetHolder;
            transform.localPosition = new Vector3(0, -0.645f,0);
            _collider.enabled = false;
            _animator.SetBool(Run, true);
            playerController.Score++;
        }
        else
        {
            GameObject targetObj = list[^1];
        
            list.Remove(targetObj);
            Destroy(targetObj);
            Destroy(gameObject);
            playerController.Score--;
        
            if (list.Count == 0)
            {
                _controllerUI.OnLevelFail?.Invoke();
            }
        }
    }
    
    private Transform TargetHolderTransform(PlayerController playerController)
    {
        Transform target = playerController.holdersTr.FirstOrDefault(m => m.transform.childCount == 1);
        
        return target != null ? target!.transform : playerController.holdersTr[^1].transform;
    }
    
    private void GetReference()
    {
        _material = GetComponentInChildren<SkinnedMeshRenderer>().material;
        _collider = GetComponent<Collider>();
        _controllerUI = UIController.Instance;
        _animator = GetComponent<Animator>();
    }
}
