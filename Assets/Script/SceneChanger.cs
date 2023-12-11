using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SceneChanger : MonoBehaviour
{
    [Tooltip("The Animator that is used for the scene transition.")]
    [SerializeField] private Animator animator;

    //Name of the scene to Load.
    private string _sceneToLoad;

    // Starts the fade-out/in animation 
    public void FadeToScene(string sceneName)
    {
        _sceneToLoad = sceneName;
        animator.SetTrigger("FadeOut");
    }

    public void FadeToScene(SceneAsset sceneAsset)
    {
        FadeToScene(sceneAsset.name);
    }

    public void FadeToScene(Scene scene)
    {
        FadeToScene(scene.name);
    }

    // Handels the actual scene Transion
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(_sceneToLoad);
    }
}
