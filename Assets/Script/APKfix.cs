using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class APKfix : MonoBehaviour
{
    private static float angle;

    public Transform xrOrigin;
    public Transform mainCamera;

    public int belongsToScene;

    public InputActionProperty actionButton;

    public TextMeshProUGUI textDisplay;
    void Start()
    {
        if(belongsToScene == 0)
        {
            Invoke("GetRotation", .05f);
        } else
        {
            xrOrigin.eulerAngles += new Vector3(0, angle, 0);
        }
    }

    private void GetRotation()
    {
        Debug.Log("Getting the rotation now");
        SetRotationUsingCamera();
        ApplyRotation();
    }

    private void SetRotationUsingCamera()
    {
        angle = 360 - mainCamera.eulerAngles.y;
    }

    private void SetRotationUsingXRORigin()
    {
        angle = xrOrigin.eulerAngles.y;
    }

    private void ApplyRotation()
    {
        xrOrigin.eulerAngles += new Vector3(0, angle, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(actionButton.action.WasPressedThisFrame())
        {
            SetRotationUsingXRORigin();
        }
        if(textDisplay != null)
        {
            textDisplay.text = "XR Origin: " + xrOrigin.eulerAngles.y + "\n Main Camera: " + mainCamera.eulerAngles.y + "\n Detected angle: " + angle;
        }
    }
}
