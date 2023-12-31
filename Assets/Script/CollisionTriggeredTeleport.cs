using System.Linq;
using UnityEngine;

// Class that triggers a Teleport when the specified collision occurs
public class CollisionTriggeredTeleport : CollisionTrigger
{
    // Inspector Editor is generated by Assets/Editor/CollisionTriggeredTeleportEditor

    public enum TeleportMode
    {
        Relative, Absolute, ToGameObject, ResetToParent
    }

    [Tooltip("Determine if the colliding Object or its parent should be transformed")]
    [SerializeField] private bool transformParent = false;

    // Variable used in Assets/Editor/CollisionTriggeredTeleportEditor
    [Tooltip("Whether the Teleportation should be relative/absolute to the objects current position or be teleported to another GameObject")]
    public TeleportMode teleportType = TeleportMode.Relative;

    [Tooltip("The Vector to be applied to the specified GameObject")]
    [SerializeField] private Vector3 teleportVector = Vector3.zero;

    [Tooltip("The Transform to teleport the specified GameObject to")]
    [SerializeField] private Transform teleportTransformDestination;

    // Start is called before the first frame update, which calls CollisionTrigger.CheckInput and checks the Teleport Destination if the Teleport Mode ToGameObject is choosen
    void Start()
    {
        base.CheckInput();
        // If this Trigger Area should teleport to a GameObject's Transform, set the Teleport Vector. If the GameObject is not set, throw an error
        if (teleportType == TeleportMode.ToGameObject)
        {
            if (teleportTransformDestination == null) throw new System.Exception("No GameObject to teleport to set in Trigger Area Teleport GameObject");
            teleportVector = teleportTransformDestination.position;
        } 
    }

    // OnTriggerEnter is called every time this GameObject's collider detects a collision with another GameObject
    private void OnTriggerEnter(Collider other)
    {
        // Detect if the other GameObject has the correct Tag or Layer (depending on the choosen detection type)
        if (detectUsing == DetectUsing.Tag)
        {
            if (!other.gameObject.CompareTag(collidingTag)) return;
        } else
        {
            if (other.gameObject.layer != _collidingLayerInt) return;
        }

        // Get the Transform component of the actual GameObject to teleport
        Transform _gameObjectToTeleport;
        if(transformParent)
        {
            _gameObjectToTeleport = other.gameObject.transform.parent;
        } else
        {
            _gameObjectToTeleport = other.gameObject.transform;
        }

        //If the Teleport Mode is ResetToParent, check if Parent and Rigidbody to reset velocity with exists
        if (teleportType == TeleportMode.ResetToParent)
        {
            if (other.gameObject.transform.parent == null) throw new System.Exception("No Parent for GameObject found to teleport to in Trigger Area Teleport GameObject");

            Rigidbody otherRigidBody = other.gameObject.GetComponent<Rigidbody>();
            if(otherRigidBody == null) throw new System.Exception("No RigidBody for GameObject found in Trigger Area Teleport GameObject");

            otherRigidBody.velocity = Vector3.zero;
            teleportVector = other.gameObject.transform.parent.position;
        }

        // Teleport relative to the current position of the GameObject
        if (teleportType == TeleportMode.Relative)
        {
            _gameObjectToTeleport.Translate(teleportVector, Space.World);
        } 
        // Teleport using Teleport Mode Absolute, ToGameObject and ResetToParent
        else
        {
            _gameObjectToTeleport.position = teleportVector;
        }
    }
}
