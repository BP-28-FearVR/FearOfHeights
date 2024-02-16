using UnityEngine;

/* This Script counters the incorrect rotation and position of the player when he loads into every scene. This incorrect offset occurs in every scene but does not accumulate.
 * Usually the first scene of the project is used for setting up the counter-offset. 
 * The bug occurs in both APK an In-Editor-Mode, though the severance is minimal in the In-Editor-Mode.
 * The incorrect offset is not present when 'Start' is called but paradoxically a short time after / when the actual first frame is shown on the Headset. 
 * In testing this took up to .3 seconds for the APK and up to 1 second for the In-Editor-Mode. 
 * In case of a setup scene, once the timer finishes this script will calculate the rotation offset from the global x-axis, save and counter it. 
 * After that this script will calculate the position offset from the global origin, save and counter it. 
 * The counter-offset will be applied to the XR_Origin_Base GameObject's Transform and not to the XR Origin GameObject's Transform as the latter shows inconsistent and unexpected
 * results for applying the offset (the XR Origin rotates around the Character Controller Component).
 */
public class RotationPositionOffsetFix : MonoBehaviour
{
    private static float rotationOffset = 0;
    private static Vector3 positionOffset = new Vector3(0,0,0);

    [Tooltip("The XR Origin Base GameObject's Transform.")]
    [SerializeField] private Transform xrOriginBaseTransform;

    [Tooltip("The XR Origin GameObject's Transform.")]
    [SerializeField] private Transform xrOriginTransform;

    [Tooltip("The Main Camera GameObject's Transform.")]
    [SerializeField] private Transform mainCameraTransform;

    [Tooltip("The Character Controller of the XR Origin Base GameObject.")]
    [SerializeField] private CharacterController characterController;

    [Tooltip("Whether this scene should be used for setting up the rotation- and position-offset.\n" +
        "ON : Will set/overwrite the rotation and position after the specified time. After that, apply the rotation- and position-counter-offset.\n" +
        "OFF: Will apply the stored rotation- and position-counter-offset after scene begins.")]
    [SerializeField] private bool sceneForOffsetSetup = false;

    [Tooltip("The time in seconds after which the offset will be calculated. (Rotating or moving the camera before the setup begins will falseify the calculated offset)" +
        "(Values tested: .3f for APK; 1f for In-Editor)")]
    [SerializeField] private float initiateSetupTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        if (xrOriginBaseTransform == null) throw new System.Exception("xrOriginBaseTransform was passed to RotationPositionOffsetFix");
        if (xrOriginTransform == null) throw new System.Exception("xrOriginTransform was passed to RotationPositionOffsetFix");
        if (mainCameraTransform == null) throw new System.Exception("mainCameraTransform was passed to RotationPositionOffsetFix");
        if (characterController == null) throw new System.Exception("characterController was passed to RotationPositionOffsetFix");

        if (sceneForOffsetSetup)
        {
            Invoke("SetUpAndApplyOffset", initiateSetupTime);
        }
        else
        {
            ApplyCounterRotation();
            ApplyCounterPosition();
        }
    }

    private void SetUpAndApplyOffset()
    {
        //Calculate and apply the rotation offset
        rotationOffset = 360 - mainCameraTransform.eulerAngles.y;
        ApplyCounterRotation();

        //Calculate and apply the position offset (includes converting the CharacterControllerCenter, which shows the position offset, to world-space)
        xrOriginTransform.localPosition = new Vector3(0, 0, 0);
        positionOffset = xrOriginTransform.TransformVector(-characterController.center);
        positionOffset.y = 0;
        ApplyCounterPosition();

    }

    private void ApplyCounterRotation()
    {
        xrOriginBaseTransform.eulerAngles += new Vector3(0, rotationOffset, 0);
    }

    private void ApplyCounterPosition()
    {
        xrOriginBaseTransform.Translate(positionOffset, Space.World);
    }
}
