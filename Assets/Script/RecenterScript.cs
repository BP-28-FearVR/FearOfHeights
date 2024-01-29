using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;

public class RecenterScript : MonoBehaviour
{

    [Tooltip("The InputActionProperty to listen for Input. It needs an interaction in its Action Properties.")]
    [SerializeField] private InputActionProperty actionButton;

    [Tooltip("The GameObjects Transform this XR Origin is going to recenter to using its position and rotation.")]
    [SerializeField] private Transform recenterPointTransform;

    private XROrigin _xrOrigin;

    private CharacterController _characterController;

    // Start is called before the first frame update
    void Start()
    {
        _xrOrigin = GetComponent<XROrigin>();
        _characterController = GetComponent<CharacterController>();
        // Registers a callback that call the Recenter function.
        actionButton.action.performed += context => Recenter();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (actionButton.action.WasPerformedThisFrame())
        {
            Recenter();
        }*/
        //Debug.Log(actionButton.action.WasPerformedThisFrame() + " vs " + actionButton.action.WasPressedThisFrame() + " vs " + actionButton.action.WasReleasedThisFrame());
    }

    public void Recenter()
    {
        Debug.Log("Recentering now...");
        _xrOrigin.MoveCameraToWorldLocation(recenterPointTransform.position + new Vector3(0,_characterController.height,0));
        _xrOrigin.MatchOriginUpCameraForward(recenterPointTransform.up, recenterPointTransform.forward);
    }
}
