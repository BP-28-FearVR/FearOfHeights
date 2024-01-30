using UnityEngine;

public class FloorDetector : MonoBehaviour
{
    //[SerializeField] private GameObject xrOriginRig;
    //private Transform _headTransform;
    [SerializeField] private float radius = 1.5f;
    private float _maxDistance = 3.0f;

    [Tooltip("The layer which is detected by the sphere cast.")]
    [SerializeField] private LayerMask layerMask;

    [Tooltip("This floor will disappear if the player walks onto it far enough.")]
    [SerializeField] private GameObject invisibleFloor;

    private RaycastHit _hit;
    private Vector3 _sphereCastDirection = new Vector3(0, -1, 0);
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (invisibleFloor != null)
        {
            bool isSpherecastColliding = Physics.SphereCast(transform.position, radius, _sphereCastDirection, out _hit, _maxDistance, layerMask);

            if (!isSpherecastColliding)
            {
                invisibleFloor.SetActive(false);
                this.gameObject.SetActive(false);
            }

        }  
    }
}
