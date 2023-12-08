using System.Collections;
using System.Collections.Generic;
using Unity.Tutorials.Core.Editor;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoSceneChanger : MonoBehaviour
{
    [Tooltip("The Scene Changer that is used for the Scene transition.")]
    [SerializeField] private SceneChanger _sceneChanger;

    [Tooltip("The time (in seconds) to wait for the AutoSceneTransition to start.")] 
    [SerializeField] private float _time = 20.0f;


    // Stores the TargetScene Name
    private static string _targetSceneName;

    // Setter for TargetScene Name
    static void SetTargetScene(string name)
    {
        _targetSceneName = name;
    }

    static void SetTargetScene(Scene scene)
    {
        SetTargetScene(scene.name);
    }

    static void SetTargetScene(SceneAsset sceneAsset)
    {
        SetTargetScene(sceneAsset.name);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_targetSceneName.IsNullOrEmpty())
        {
            _targetSceneName = "Scene2";
        }

        Invoke("OnTimerDone", _time);
    }

    void OnTimerDone()
    {
        _sceneChanger.FadeToScene(_targetSceneName);
    }
}
