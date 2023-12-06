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
        // execute minimal movement on each physic update to trigger collision detection of the character controller
        // player will not notice the movement
        _characterController.Move(new Vector3(0.001f, -0.001f, 0.001f));
        _characterController.Move(new Vector3(-0.001f, 0.001f, -0.001f));
    }
}
