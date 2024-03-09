using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OutlineOnHover : MonoBehaviour
{
    [Tooltip("Grab Interactor of this GameObject")]
    [SerializeField] private XRGrabInteractable xrGrabInteractable;

    [Tooltip("Outline Instance used by this script")]
    [SerializeField] private Outline outline;

    //Added the Event Listeners to the XRGrabInteractable
    void Start ()
    {
        xrGrabInteractable = GetComponent<XRGrabInteractable>();

        outline = GetComponent<Outline>();
    }

    void Update()
    {
        if (xrGrabInteractable == null || outline == null) return;

        if (!xrGrabInteractable.isHovered) { outline.TurnOutlineOff(); return; }
        

        Boolean outlineOn = false;
        foreach (XRDirectInteractor interactor in xrGrabInteractable.interactorsHovering)
        {
            if (interactor != null && !interactor.IsSelecting(xrGrabInteractable))
            {
                outlineOn = interactor.targetsForSelection != null &&
                    interactor.targetsForSelection.Count > 0 &&
                    GameObject.ReferenceEquals(xrGrabInteractable,interactor.targetsForSelection[0]);
            }
        }

        if (outlineOn)
        {
            outline.TurnOutlineOn();
        } else
        {
            outline.TurnOutlineOff();
        }
    }
}
