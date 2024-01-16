using UnityEngine;

public class CharacterControllerColissionDetection : MonoBehaviour
{
    private CharacterController _characterController;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {

    }
}
