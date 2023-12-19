using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class OpenDoor : MonoBehaviour
{
    [Tooltip("The door to open")]
    [SerializeField] private GameObject door;

    [Tooltip("Opening Door Speed")]
    [SerializeField] private float openSpeed = 0.01f;

    [Tooltip("Final Door Rotation")]
    [SerializeField] private float openRotation = 84.0f;

    private bool _isDoorMoving = false;
    private bool _isDoorOpened = false;


    // Opens the door if it's not moving or open
    public void Open()
    {
        if (!_isDoorMoving && !_isDoorOpened)
        {
            StartCoroutine(RotateDoor(openRotation));
            _isDoorOpened = true;
        }
    }

    // Closes the door if it's not moving or closed
    public void Close()
    {
        if (!_isDoorMoving && _isDoorOpened)
        {
            StartCoroutine(RotateDoor(0.0f));
            _isDoorOpened = false;
        }
    }

    // Rotates the door to a given target angle
    IEnumerator RotateDoor(float target)
    {
        _isDoorMoving = true;

        float initialRotation = door.transform.rotation.eulerAngles.z;
        float elapsedTime = 0.0f;

        // Calculates the new Rotation for each tick until the animation time has elapsed and reached the target angle
        while (elapsedTime < 1.0f)
        {
            float newRotation = Mathf.Lerp(initialRotation, target, elapsedTime);
            door.transform.rotation = Quaternion.Euler(-90, 0, newRotation);
            elapsedTime += Time.deltaTime * openSpeed;
            yield return null;
        }

        door.transform.rotation = Quaternion.Euler(-90, 0, target);
        _isDoorMoving = false;
    }
}
