using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Used to start, follow it's progress and end the experiment
public class ExperimentController : MonoBehaviour
{
    [Tooltip("The SceneChanger to call if the experiment has ended")]
    [SerializeField] private SceneChanger sceneChanger;

    [Tooltip("The Scene to switch to if the experiment has ended")]
    [SerializeField] private SceneAsset nextScene;

    [Tooltip("Empty GameObjects each containing 1 Item as child that is part of the Experiment, will be spawned and must be collected")]
    [SerializeField] private List<GameObject> experimentItems;

    // Enumerator to enumerate thorugh all the Experiment Items and spawn them
    private List<GameObject>.Enumerator enumerator;

    private int _itemsSpawned = 0;

    private int _itemsCollected = 0;

    private bool _IsExperimentOngoing = true;

    private string _collidingLayer = "ExperimentItem";

    // Layer comparison is done in int, calculation result of the layer conversion to int is saved
    private int _collidingLayerInt = -1;

    // Start is called before the first frame update
    void Start()
    {
        _collidingLayerInt = LayerMask.NameToLayer(_collidingLayer);
        // Does a layer with that name exist (-1 means an non-existant layer)
        if (_collidingLayerInt == -1) throw new System.Exception("Unregistered Layer '" + _collidingLayer + "' used in ExperimentController");

        // Check if SceneChanger is set
        if (sceneChanger == null) throw new System.Exception("No SceneChanger instance passed for ExperimentController");

        // Check if every GameObject in experimentItems is an actual Experiment Item
        foreach (GameObject currentObject in experimentItems)
        {
            if (!IsExperimentItem(currentObject)) throw new System.Exception("GameObject passed to List that is not an ExperimentItem");
        }

        // Set Enumerator. First Item is null, Start at the second item so it has to move to the first item
        enumerator = experimentItems.GetEnumerator();
        enumerator.MoveNext();

        //Spawn first Experiment Item
        SpawnNextItem();
    }

    // OnTriggerEnter is called every time this GameObject's collider detects a collision with another GameObject
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == null) return;

        // Handle it if the colliding GameObject is an Experiment Item
        if(IsExperimentItem(other.gameObject))
        {
            _itemsCollected++;
            if(CheckIfExperimentEnded())
            {
                EndExperiment();
            } else
            {
                // Check if all Experiment Items that were spawned are collected
                if(_itemsCollected == _itemsSpawned)
                {
                    SpawnNextItem();
                }
            }
        }
    }

    // OnTriggerExit is called every time this GameObject's collider detects that a collision with another GameObject has ended
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == null) return;

        // Handle it if the no longer colliding GameObject is an Experiment Item
        if (IsExperimentItem(other.gameObject))
        {
            _itemsCollected--;
        }
    }

    // Check if the experiment has ended (=: It is still running but the Success Condition has been met)
    private bool CheckIfExperimentEnded()
    {
        return (_IsExperimentOngoing && experimentItems.Count == _itemsCollected);
    }

    // Check if the specified GameObject is an ExperimentItem defined by it's layer being 'ExperimentItem'
    private bool IsExperimentItem(GameObject objectToCheck)
    {
        return objectToCheck.layer == _collidingLayerInt;
    }

    // End the Experiment and initiate the Switch to the next Scene
    private void EndExperiment()
    {
        _IsExperimentOngoing = false;
        sceneChanger.FadeToScene(nextScene);
    }

    // Activate the next Experiment Item and move the Enumerator onto the next Experiment Item
    public void SpawnNextItem()
    {
        enumerator.Current.SetActive(true);
        enumerator.MoveNext();
        _itemsSpawned++;
    }
}
