using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEditor;

public class SceneChanger : MonoBehaviour
{
    public Animator animator;

    private string sceneToLoad;

    public void FadeToScene(SceneAsset sceneAsset)
    {

        sceneToLoad = sceneAsset.name;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
