using UnityEngine;

public class ColorRoad : MonoBehaviour, IInteractable
{
    [SerializeField] private bool isStartPoint;
    [SerializeField] private TurretController turretController;
    
    public void Execute(PlayerController playerController)
    {
        if (isStartPoint)
        {
            playerController.tempVerticalSpeed /= 2;
            playerController.OnPlay += turretController.Fire;
        }
        else
        {
            playerController.tempVerticalSpeed *= 2;
            playerController.OnPlay -= turretController.Fire;
        }
    }
}
