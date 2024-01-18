using UnityEngine;

public class TorchHandler : MonoBehaviour
{
    [Tooltip("The Torch Ridgdbody that should fall down.")]
    [SerializeField] private Rigidbody torch;

    //Handles throwing of the torch when player is to close.
    public void OnPlayerToClose()
    {
        if (torch != null)
        {
            torch.AddRelativeTorque(5, 0, 0, ForceMode.VelocityChange);
        }
        

        this.enabled = false;
        this.gameObject.SetActive(false);
    }
}
