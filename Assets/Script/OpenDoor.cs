using System.Collections;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [Tooltip("The door to open")]
    [SerializeField] private GameObject door;

    [Tooltip("Opening Door Speed")]
    [SerializeField] private float openSpeed = 0.01f;

    [Tooltip("Final Door Rotation")]
    [SerializeField] private float openRotation = 84.0f;

    [Tooltip("The time in seconds after which the door can be opened")]
    [SerializeField] private float timeUntilDoorOpens;

    [Tooltip("The trigger area for opening this door")]
    [SerializeField] private GameObject doorTrigger;

    private float _timer = 0.0f;
    private bool _hasUiBeenClosed = false;
    private bool _isDoorTriggerActive = false;
    private bool _isDoorMoving = false;
    private bool _isDoorOpened = false;

    [SerializeField] private Outline outline;

    public void Update()
    {
        if (_hasUiBeenClosed && !_isDoorTriggerActive)
        {
            _timer += Time.deltaTime;
            if (_timer >= timeUntilDoorOpens)
            {
                doorTrigger.SetActive(true);
                _isDoorTriggerActive = true;
                outline.TurnOutlineOn();
            }
        }
    }

    public void SetIsUiClosed(bool isUiClosed)
    {
        _hasUiBeenClosed = isUiClosed;
    }

    // Opens door if closed and closes door if opened
    public void Interact()
    {
        if (!_isDoorOpened)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

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
        // Start Rotation
        _isDoorMoving = true;
        Quaternion initialRotation = door.transform.rotation;
        float elapsedTime = 0.0f;

        // Calculates the new Rotation for each tick until the animation time has elapsed and reached the target angle
        while (elapsedTime < 1.0f)
        {
            door.transform.rotation = Quaternion.Lerp(initialRotation, Quaternion.Euler(-90, 0, target), elapsedTime);
            elapsedTime += Time.deltaTime * openSpeed;
            yield return null;
        }

        // Apply final rotation
        door.transform.rotation = Quaternion.Euler(-90, 0, target);
        _isDoorMoving = false;
    }
}
