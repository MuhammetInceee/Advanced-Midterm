using UnityEngine;

public class ColorRoad : MonoBehaviour, IInteractable
{
    private static readonly int Crouch = Animator.StringToHash("Crouch");

    
    [SerializeField] private bool isStartPoint;
    [SerializeField] private TurretController turretController;
    
    public void Execute(PlayerController playerController)
    {
        if (isStartPoint)
        {
            playerController.tempVerticalSpeed /= 2;
            playerController.OnPlay += turretController.Fire;
            playerController.ListAnimationControl(Crouch, true);
        }
        else
        {
            playerController.tempVerticalSpeed *= 2;
            playerController.OnPlay -= turretController.Fire;
            playerController.ListAnimationControl(Crouch, false);
        }
    }
}
