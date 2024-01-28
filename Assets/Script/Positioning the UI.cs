using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class PositioningtheUI : MonoBehaviour
{
    public XRNode xrNode = XRNode.Head; // XRNode f�r die Position des Nutzers
    public float distanceFromUser = 2f; // Abstand zwischen Nutzer und UI
    public float distanceInFront = 3f; // Zus�tzlicher Abstand vor dem Nutzer

    private Vector3 initialPosition; // Die urspr�ngliche Position des UI-Elements

    void Start()
    {
        // �berpr�fe, ob XRNode g�ltig ist
        if (XRSettings.isDeviceActive)
        {
            // Erhalte die Position und Rotation des XR Nodes (z.B., Headset)
            InputTracking.GetNodeStates(new List<XRNodeState>());
            Vector3 headsetPosition = InputTracking.GetLocalPosition(xrNode);
            Quaternion headsetRotation = InputTracking.GetLocalRotation(xrNode);

            // Berechne die Zielposition basierend auf der Nutzerposition, der Blickrichtung und den Abst�nden
            Vector3 targetPosition = headsetPosition + (headsetRotation * Vector3.forward * distanceFromUser) +
                                     (headsetRotation * Vector3.forward * distanceInFront);

            // Setze die UI-Position auf die berechnete Zielposition
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
