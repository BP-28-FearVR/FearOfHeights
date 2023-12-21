using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

// This class handles switching between Scenes with a fade-out and fade-in effect
public class SceneChanger : MonoBehaviour
{
    [Tooltip("The Animator that is used for the scene transition.")]
    [SerializeField] private Animator animator;

    // Name of the scene to Load.
    private string _sceneToLoad;

    // Starts the fade-out/in animation 
    private void FadeToScene(string sceneName)
    {
        _sceneToLoad = sceneName;
        animator.SetTrigger("FadeOut");
    }

    // The function to be called in order to initiate a Scene Change (The parameter's datatype makes sure no invalid input except null is passed)
    public void FadeToScene(SceneAsset sceneAsset)
    {
        if (sceneAsset == null) throw new System.Exception("No SceneAsset was passed to SceneChanger");
        FadeToScene(sceneAsset.name);
    }

    // Handles the actual scene Transion
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(_sceneToLoad);
    }
}
