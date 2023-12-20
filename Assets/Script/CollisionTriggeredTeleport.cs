using System.Linq;
using UnityEngine;

// Class that triggers a Teleport when the specified collision occurs
public class CollisionTriggeredTeleport : CollisionTrigger
{
    // Inspector Editor is generated by Assets/Editor/CollisionTriggeredTeleportEditor

    public enum TeleportMode
    {
        Relative, Absolute, ToGameObject
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

    // Start is called before the first frame update, which calls CollisionTrigger.CheckInput
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
        // Teleport relative to the current position of the GameObject, the absolute position or another GameObject in the World
        if(teleportType == TeleportMode.Relative)
        {
            _gameObjectToTeleport.Translate(teleportVector, Space.World);
        } 
        //this path handles both absolute and teleportToGameObject
        else
        {
            _gameObjectToTeleport.position = teleportVector;
        }
    }
}
