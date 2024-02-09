using UnityEngine;
using UnityEngine.XR;

public class PositioningtheUI : MonoBehaviour
{
    [SerializeField]
    private XRNode xrNode = XRNode.Head; // XRNode for user position and rotation

    [SerializeField]
    private float distanceFromUser = 3.5f; // Distance of the UI from the user

    [SerializeField]
    private float heightFromGround = 2f; // Height above the ground where the UI should appear

    private void Start()
    {
        if (XRSettings.isDeviceActive)
        {
            if (TrySetUIPositionAndRotation())
            Debug.LogWarning("Failed to set UI position and rotation.");
        }
    }

    private bool TrySetUIPositionAndRotation()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(xrNode);
        if (device == null)
            return false;

        Vector3 headsetPosition;
        if (!device.TryGetFeatureValue(CommonUsages.devicePosition, out headsetPosition))
            return false;

        Quaternion headsetRotation;
        if (!device.TryGetFeatureValue(CommonUsages.deviceRotation, out headsetRotation))
            return false;

        Vector3 targetPosition = headsetPosition + (headsetRotation * Vector3.forward * distanceFromUser);
        targetPosition.y = heightFromGround;

        transform.position = targetPosition;

        // Set the UI rotation to the XR Node rotation (horizontal only)
        Quaternion horizontalRotation = Quaternion.Euler(0f, headsetRotation.eulerAngles.y, 0f);
        transform.rotation = horizontalRotation;

        return true;
    }
}

