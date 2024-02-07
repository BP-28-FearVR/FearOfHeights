using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class PositioningTheUI : MonoBehaviour
{
    public XRNode xrNode = XRNode.Head; // XRNode for user position and rotation
    public float distanceFromUser = 2f; // Distance of the UI from the user
    public float heightFromGround = 1.5f; // Height above the ground where the UI should appear


    void Start()
    {
        // Check if XRNode is valid
        if (XRSettings.isDeviceActive)
        {
            // Get the position and rotation of the XR Node (e.g., headset)
            InputTracking.GetNodeStates(new List<XRNodeState>());
            Vector3 headsetPosition = InputTracking.GetLocalPosition(xrNode);
            Quaternion headsetRotation = InputTracking.GetLocalRotation(xrNode);

            // Calculate the target position based on the position and rotation of the XR Node
            Vector3 targetPosition = headsetPosition + (headsetRotation * Vector3.forward * distanceFromUser);

            // Set the height of the UI position to the desired height above the ground
            targetPosition.y = heightFromGround;

            // Set the UI position to the calculated target position
            transform.position = targetPosition;

            // Calculate a horizontal rotation based on the user's rotation
            Quaternion horizontalRotation = Quaternion.Euler(0f, headsetRotation.eulerAngles.y, 0f);

            // Set the UI rotation to the horizontal rotation
            transform.rotation = horizontalRotation;

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
