using UnityEngine;

public class FloorDetector : MonoBehaviour
{
    [Tooltip("The radius of the Spherecast")]
    [SerializeField] private float radius = 1.5f;

    [Tooltip("The layer which is detected by the Spherecast.")]
    [SerializeField] private LayerMask layerMask;

    [Tooltip("This object will disappear if the player leaves the layer which is set in layer mask")]
    [SerializeField] private GameObject invisibleFloor;

    private RaycastHit _hit;
    private Vector3 _sphereCastDirection = new Vector3(0, -1, 0);
    private float _maxDistance = 3.0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (invisibleFloor != null)
        {
            DisableFloor();
        }
    }

    // if the player leaves the specified layer, the floor will disappear, causing the player to fall down
    public void DisableFloor()
    {
        bool isSpherecastColliding = Physics.SphereCast(transform.position, radius, _sphereCastDirection, out _hit, _maxDistance, layerMask);

        if (!isSpherecastColliding)
        {
            invisibleFloor.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
