using System.Collections.Generic;
using UnityEngine;

public class ExperimentController : MonoBehaviour
{
    [Tooltip("Capsules each containing 1 Item that is part of the Experiment, will be spawned and must be collected")]
    [SerializeField] private List<GameObject> experimentItems;

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
        if (_collidingLayerInt == -1)
        {
            throw new System.Exception("Unregistered Layer '" + _collidingLayer + "' used in ExperimentController");
        }
        foreach(GameObject currentObject in experimentItems)
        {
            if (currentObject.GetComponent<ExperimentItem>() == null) throw new System.Exception("GameObject passed to List that is not an ExperimentItem");
        }
        enumerator = experimentItems.GetEnumerator();
        enumerator.MoveNext();
        SpawnNextItem();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == null) return;

        ExperimentItem item = other.gameObject.GetComponent<ExperimentItem>();
        if(item != null)
        {
            _itemsCollected++;
            if(CheckIfExperimentEnded())
            {
                EndExperiment();
            } else
            {
                if(_itemsCollected == _itemsSpawned)
                {
                    SpawnNextItem();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == null) return;

        ExperimentItem item = other.gameObject.GetComponent<ExperimentItem>();
        if (item != null)
        {
            _itemsCollected--;
        }
    }

    private bool CheckIfExperimentEnded()
    {
        return (_IsExperimentOngoing && experimentItems.Count == _itemsCollected);
    }

    private void EndExperiment()
    {
        _IsExperimentOngoing = false;
        Debug.Log("Experiment has ended");
    }

    public void SpawnNextItem()
    {
        enumerator.Current.SetActive(true);
        enumerator.MoveNext();
        _itemsSpawned++;
    }
}
