using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class APKfix : MonoBehaviour
{
    private static float angle;
    private static Vector3 positionOffset;

    public Transform xrOriginBasePrefab;
    public Transform xrOrigin;
    public Transform mainCamera;
    public CharacterController charController;

    public int belongsToScene;

    public InputActionProperty actionButton;
    public InputActionProperty primaryButton;

    public TextMeshProUGUI textDisplay;

    public GameObject recenterPoint;
    void Start()
    {
        if(belongsToScene == 0)
        {
            Invoke("GetRotation", 1f);
            //GetRotation();
        } else
        {
            xrOriginBasePrefab.eulerAngles += new Vector3(0, angle, 0);
            xrOriginBasePrefab.Translate(positionOffset, Space.World);
        }
    }

    private void GetRotation()
    {
        Debug.Log("Getting the rotation now");
        SetRotationUsingCamera();
        ApplyRotation(); //Rotation zerstört Arbeit vom Rücksetzen der Position. Vielleicht XR Origin nicht zentriert obwohl oben auf localposition (0,0,0) gesetzt wurde?
        xrOrigin.localPosition = new Vector3(0, 0, 0);
        positionOffset = xrOrigin.TransformVector(-charController.center);
        positionOffset.y = 0;
        xrOriginBasePrefab.Translate(positionOffset, Space.World);
        
    }

    private void SetRotationUsingCamera()
    {
        angle = 360 - mainCamera.eulerAngles.y;
        Debug.Log(angle);
    }

    private void SetRotationUsingXRORigin()
    {
        angle = xrOrigin.eulerAngles.y;
    }

    private void ApplyRotation()
    {
        xrOriginBasePrefab.eulerAngles += new Vector3(0, angle, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(actionButton.action.WasPressedThisFrame())
        {
            xrOriginBasePrefab.Translate(positionOffset, Space.World);
        }
        if (primaryButton.action.WasPressedThisFrame())
        {
            xrOrigin.localPosition = new Vector3(0, 0, 0);
        }
        if (textDisplay != null)
        {
            /*textDisplay.text = "XR Origin: " + xrOrigin.eulerAngles.y + "\n" +
                "\n RecenterPoint Position: " + recenterPoint.transform.position + "\n Position Offset: " + positionOffset + "\n XR Origin Position: " + xrOrigin.position
                + "\n Character Controller: " + charController.center;*/
            textDisplay.text = "XROriginPrefab Pos Abs: " + xrOriginBasePrefab.position + "\n XROriginPrefab Pos Rel: " + xrOriginBasePrefab.localPosition +
                 "\n XROriginPrefab Rot Abs: " + xrOriginBasePrefab.eulerAngles + "\n XROriginPrefab Rot Rel: " + xrOriginBasePrefab.localEulerAngles +
                 "\n XROrigin Pos Abs: " + xrOrigin.transform.position + "\n XROrigin Pos Rel: " + xrOrigin.transform.localPosition +
                 "\n XROrigin Rot Abs: " + xrOrigin.transform.eulerAngles + "\n XROrigin Rot Rel: " + xrOrigin.transform.localEulerAngles +
                 "\n Camera Pos Abs: " + mainCamera.transform.position + "\n Camera Pos Rel: " + mainCamera.transform.localPosition +
                 "\n Camera Rot Abs: " + mainCamera.transform.eulerAngles + "\n Camera Rot Rel: " + mainCamera.transform.localEulerAngles + "\n" + 
                 "\n CharacterController Pos: " + charController.center + "\n Position Offset: " + positionOffset + "\n Angle: " + angle;
        }
    }
}
