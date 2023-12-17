using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using FIMSpace.FTail;
using UnityEngine;

public class CityEnter : MonoBehaviour, IInteractable
{
    public void Execute(PlayerController playerController)
    {
        playerController.OnCityEnter?.Invoke();
        
        StartCoroutine(OneBigStickMan(playerController));
    }

    private IEnumerator OneBigStickMan(PlayerController playerController)
    {
        var list = playerController.stackList;
        playerController.rope.GetComponentInChildren<TailAnimator2>().enabled = false;

        foreach (GameObject obj in list)
        {
            obj.transform.SetParent(null);
        }
        
        for (int i = 1; i < list.Count; i++)
        {
            GameObject targetObj = list[i];
            
            targetObj.transform.DOJump(list[0].transform.localPosition, 1f, 1, 0.5f)
                .OnComplete(() =>
                {
                    targetObj.SetActive(false);
                    list[0].transform.localScale += Vector3.one * 0.2f;
                });
            
            yield return new WaitForSeconds(0.2f);
        }
        
        playerController.rope.SetActive(false);
    }
}
