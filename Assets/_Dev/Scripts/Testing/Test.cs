using UnityEngine;

public class Test : MonoBehaviour, IInteractable
{
    public void Execute(PlayerController playerController)
    {
        playerController.OnCityEnter?.Invoke();
    }
}
