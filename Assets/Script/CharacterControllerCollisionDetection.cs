using UnityEngine;

public class CharacterControllerColissionDetection : MonoBehaviour
{
    private CharacterController _characterController;

    private readonly static Vector3 _small_move = new(0.001f, -0.001f, 0.001f);
    private readonly static Vector3 _small_move_back = new(-0.001f, 0.001f, -0.001f);

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        // execute minimal movement on each physic update to trigger collision detection of the character controller
        // player will not notice the movement
        _characterController.Move(_small_move);
        _characterController.Move(_small_move_back);
    }
}
