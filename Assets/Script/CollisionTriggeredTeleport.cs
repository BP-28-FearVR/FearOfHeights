using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollisionTriggeredTeleport : MonoBehaviour
{
    //UI wird durch Assets/Editor/CollisionTriggeredTeleportEditor gestellt

    public enum DetectUsing
    {
        Tag, Layer
    }

    public enum TeleportMode
    {
        Relative, Absolute
    }

    private static string defaultTagName = "Untagged";
    private static string defaultLayerName = "Default";

    [Tooltip("Determine if the colliding Object or its parent should be transformed")]
    [SerializeField] private bool _transformParent = false;

    [Tooltip("Whether the Teleportation should be relative to the objects current position or absolute")]
    [SerializeField] private TeleportMode _vectorType = TeleportMode.Relative;

    [Tooltip("The position to teleport to in absolute coordinates")]
    [SerializeField] private Vector3 _teleportVector = Vector3.zero;

    [Tooltip("What to detect the colliding Object by")]
    public DetectUsing detectUsing = DetectUsing.Tag;

    [Tooltip("The tag collision has to be detected for")]
    [SerializeField] private string _collidingTag = defaultTagName;

    [Tooltip("The layer collision has to be detected for")]
    [SerializeField] private string _collidingLayer = defaultLayerName;

    private int _collidingLayerInt = -1;


    // Start is called before the first frame update
    void Start()
    {
        if (detectUsing == DetectUsing.Tag)
        {
            if (!UnityEditorInternal.InternalEditorUtility.tags.Contains<string>(_collidingTag))
            {
                throw new System.Exception("Unregistered Tag used in Trigger Area GameObject");
            }
        } else
        {
            _collidingLayerInt = LayerMask.NameToLayer(_collidingLayer);
            if (_collidingLayerInt == -1)
            {
                throw new System.Exception("Unregistered Layer used in Trigger Area GameObject");
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (detectUsing == DetectUsing.Tag)
        {
            if (!other.gameObject.CompareTag(_collidingTag)) return;
        } else
        {
            if (other.gameObject.layer != _collidingLayerInt) return;
        }

        Transform _gameObjectToTeleport;
        
        if(_transformParent)
        {
            _gameObjectToTeleport = other.gameObject.GetComponentInParent<Transform>();
        } else
        {
            _gameObjectToTeleport = other.gameObject.GetComponent<Transform>();
        }
        
        if(_vectorType == TeleportMode.Relative)
        {
            _gameObjectToTeleport.Translate(_teleportVector);
        } else
        {
            _gameObjectToTeleport.position = _teleportVector;
        }
        
    }
}
