using System.Collections;
using System.Collections.Generic;
//using System.Linq;
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


    // Start is called before the first frame update
    void Start()
    {
        /*Debug.Log("Bis hier hin komme ich");
        if (!UnityEditorInternal.InternalEditorUtility.tags.Contains<string>(_collidingTag))
        {
            Debug.Log("Fehler!");
        }*/
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInParent<Transform>().Translate(new Vector3(0, 0, 20));
        }
    }
}
