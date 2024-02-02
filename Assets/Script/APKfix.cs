using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class APKfix : MonoBehaviour
{
    public Transform trans1;
    public Transform trans2;

    public TextMeshProUGUI textDisplay;

    public Transform cam;
    public Transform recenterPoint;
    public Transform xrorigin;

    private float startingAngle;

    private static float angle;

    public int instance;

    public InputActionProperty actionButton;
    void Start()
    {
        if(instance == 0)
        {
            Invoke("GetRotation", .05f);
        } else
        {
            xrorigin.eulerAngles += new Vector3(0, angle, 0);
        }
    }

    private void GetRotation()
    {
        Debug.Log("Getting the rotation now");
        startingAngle = trans2.eulerAngles.y;
        SetRotationUsingCamera();
        ApplyRotation();
    }

    private void SetRotationUsingCamera()
    {
        angle = 360 - trans2.eulerAngles.y;
    }

    private void SetRotationUsingXRORigin()
    {
        angle = xrorigin.eulerAngles.y;
    }

    private void ApplyRotation()
    {
        xrorigin.eulerAngles += new Vector3(0, angle, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(actionButton.action.WasPressedThisFrame())
        {
            SetRotationUsingXRORigin();
        }
        if(instance != 1)
        {
            textDisplay.text = "XR Origin: " + trans1.eulerAngles.y + "\n Main Camera: " + trans2.eulerAngles.y + "\n Detected angle: " + angle;
        }
    }
}
