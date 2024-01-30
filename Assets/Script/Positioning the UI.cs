using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class PositioningtheUI : MonoBehaviour
{
    public XRNode xrNode = XRNode.Head; // XRNode for the user's position
    public float distanceFromUser = 2f; // Distance between the user and the UI
    public float distanceInFront = 3f; // Additional distance in front of the user

    private Vector3 initialPosition; // The original position of the UI element

    void Start()
    {
        // Check if XRNode is valid
        if (XRSettings.isDeviceActive)
        {
            // Get the position and rotation of the XR Node (e.g., headset)
            InputTracking.GetNodeStates(new List<XRNodeState>());
            Vector3 headsetPosition = InputTracking.GetLocalPosition(xrNode);
            Quaternion headsetRotation = InputTracking.GetLocalRotation(xrNode);

            // Calculate the target position based on the user's position, gaze direction, and distances
            Vector3 targetPosition = headsetPosition + (headsetRotation * Vector3.forward * distanceFromUser) +
                                     (headsetRotation * Vector3.forward * distanceInFront);

            // Set the UI position to the calculated target position
            transform.position = targetPosition;
        }
        else
        {
            Debug.LogWarning("XR device not active. Please ensure your XR device is connected and supported.");
        }
    }

    void Update()
    {
      
    }
}
