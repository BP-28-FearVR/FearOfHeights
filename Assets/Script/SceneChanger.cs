using UnityEngine;
using UnityEngine.SceneManagement;

// This class handles switching between Scenes with a fade-out and fade-in effect
public class SceneChanger : MonoBehaviour
{
    [Tooltip("The Animator that is used for the scene transition.")]
    [SerializeField] private Animator animator;

    // Build Indext of the scene to Load.
    private SceneObject _sceneToLoad;

    // Starts the fade-out/in animation 
    public void FadeToScene(SceneObject scene)
    {
        if (scene == null) throw new System.Exception("No scene was passed to SceneChanger");

        _sceneToLoad = scene;
        animator.SetTrigger("FadeOut");
    }

    // Handles the actual scene Transion
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(_sceneToLoad);
    }
}
