using System.Linq;
using UnityEngine;

// Class that triggers a Teleport when the specified collision occurs
public class CollisionTriggeredTeleport : CollisionTrigger
{
    // Inspector Editor is generated by Assets/Editor/CollisionTriggeredTeleportEditor

    public enum TeleportMode
    {
        Relative, Absolute
    }

    [Tooltip("Determine if the colliding Object or its parent should be transformed")]
    [SerializeField] private bool _transformParent = false;

    [Tooltip("Whether the Teleportation should be relative to the objects current position or absolute")]
    [SerializeField] private TeleportMode _vectorType = TeleportMode.Relative;

    [Tooltip("The Vector to be applied to the specified GameObject")]
    [SerializeField] private Vector3 _teleportVector = Vector3.zero;

    // Start is called before the first frame update, which calls CollisionTrigger.CheckInput
    void Start()
    {
        base.CheckInput();
    }

    // OnTriggerEnter is called every time this GameObject's collider detects a collision with another GameObject
    private void OnTriggerEnter(Collider other)
    {
        // Detect if the other GameObject has the correct Tag or Layer (depending on the choosen detection type)
        if (detectUsing == DetectUsing.Tag)
        {
            if (!other.gameObject.CompareTag(_collidingTag)) return;
        } else
        {
            if (other.gameObject.layer != _collidingLayerInt) return;
        }

        // Get the Transform component of the actual GameObject to teleport
        Transform _gameObjectToTeleport;
        
        if(_transformParent)
        {
            _gameObjectToTeleport = other.gameObject.GetComponentInParent<Transform>();
        } else
        {
            _gameObjectToTeleport = other.gameObject.GetComponent<Transform>();
        }
        
        // Teleport relative to the current position of the GameObject or the absolute position in the World
        if(_vectorType == TeleportMode.Relative)
        {
            _gameObjectToTeleport.Translate(_teleportVector);
        } else
        {
            _gameObjectToTeleport.position = _teleportVector;
        }
    }
}
