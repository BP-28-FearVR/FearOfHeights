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
    public void FadeToScene(SceneAsset sceneAsset)
    {
        _sceneToLoad = sceneAsset.name;
        animator.SetTrigger("FadeOut");
    }
    
    // Handels the actual scene Transion
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(_sceneToLoad);
    }
}
