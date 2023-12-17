using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class HouseBuy : MonoBehaviour, IInteractable
{
    private bool _buying;
    private bool _isFilled;
    private int _currentCount;
    [SerializeField] private int requireCount;
    [SerializeField] private TextMeshProUGUI text;

    private string TextContent => text.text = $"{_currentCount} / {requireCount}";

    private int CurrentCount
    {
        get => _currentCount;
        set
        {
            _currentCount = value;
            text.text = TextContent;

            if (_currentCount >= requireCount)
            {
                print("Congs");
                _isFilled = true;
            }
        }
    }

    private void Awake()
    {
        InitVariables();
    }



    public void Execute(PlayerController playerController)
    {
        _buying = true;
        StartCoroutine(GiveHuman(playerController));
    }
    
    public void ExecuteExit(PlayerController playerController)
    {
        _buying = false;
    }

    private IEnumerator GiveHuman(PlayerController playerController)
    {
        var list = playerController.stackList;
        
        while (_buying && list.Count > 0 && !_isFilled)
        {
            GameObject targetObj = list[0];
            
            targetObj.transform.position = playerController.transform.position;
            targetObj.SetActive(true);
            targetObj.transform.DOJump(transform.position, 1, 1, 0.5f)
                .OnComplete(() =>
                {
                    playerController.transform.localScale -= Vector3.one * 0.2f;
                    list.Remove(targetObj);
                    Destroy(targetObj);
                    CurrentCount++;
                });
            yield return new WaitForSeconds(0.7f);
        }
    }
    
    private void InitVariables()
    {
        text.text = TextContent;
    }
}
