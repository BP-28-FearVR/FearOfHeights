using UnityEngine;

// Used to call the SceneChanger after a specified time in order to switch to a specified Scene
public class AutoSceneChanger : MonoBehaviour
{
    [Tooltip("The Scene Changer that is used for the Scene transition.")]
    [SerializeField] private SceneChanger sceneChanger;

    [Tooltip("The time (in seconds) to wait for the AutoSceneTransition to start.")] 
    [SerializeField] private float time = 20.0f;

    // Stores the TargetScene Name
    [Tooltip("The time (in seconds) to wait for the AutoSceneTransition to start.")]
    [SerializeField] private SceneObject targetScene;

    // Setter for TargetScene Name
    public void SetTargetScene(SceneObject sceneObject)
    {
        targetScene = sceneObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("OnTimerDone", time);
    }

    // Is called once the timer finishes
    void OnTimerDone()
    {
        sceneChanger.FadeToScene(targetScene);
    }
}
