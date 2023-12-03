using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClippingDetector : MonoBehaviour
{
    public Animator animator;
    public string forbiddenLayerName;

    private int amountClippedThrough = 0;
    private int forbiddenLayerIndex;

    private void Awake()
    {
        forbiddenLayerIndex = LayerMask.NameToLayer(forbiddenLayerName);
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision Detected his layer being " + other.gameObject.layer + " and the forbidden layer being " + forbiddenLayerIndex);
        if (other.gameObject.layer == forbiddenLayerIndex)
        {
            Debug.Log("Collision with forbidden Object detected!");
            if(amountClippedThrough == 0)
            {
                Debug.Log("Fading Screen to Black!");
                animator.SetTrigger("ClippingStarted");
            }
            amountClippedThrough++;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("Collision ended");
        if (other.gameObject.layer == forbiddenLayerIndex)
        {
            Debug.Log("Collision with forbidden Object ended!");
            amountClippedThrough--;
            if (amountClippedThrough == 0)
            {
                Debug.Log("Fading Screen to White!");
                animator.SetTrigger("ClippingEnded");
            }
        }
    }
}
