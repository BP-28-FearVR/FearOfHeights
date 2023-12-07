using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DoorOpener : MonoBehaviour
{
    [Tooltip("Angle of the fully opened door.")]
    [SerializeField] private float _openDoorAngle;

    [Tooltip("The inner part of the door that will be opened.")]
    [SerializeField] private GameObject _door;

    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        animator.keepAnimatorStateOnDisable = true;
        Invoke("OpenDoor", 20.0f);
    }

    void OpenDoor()
    {
        animator.SetFloat("Rotation", _openDoorAngle);
    }

}
