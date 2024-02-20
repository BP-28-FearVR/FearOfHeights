using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DevModeMenu : MonoBehaviour
{
    [Tooltip("The DevModeMenu GameObject")]
    [SerializeField] private GameObject devMenu;

    // Start is called before the first frame update
    void Start()
    {
        if(devMenu != null)
        {
            Button[] buttons = devMenu.GetComponentsInChildren<Button>();
            Debug.Log(buttons);
            foreach(Button button in buttons)
            {
                if (!hasListenerAttached(button.onClick)) 
                {
                    button.interactable = false;
                }
            }
        }
    }

    private bool hasListenerAttached(UnityEvent eventToCheck) {
        bool result = false;
        for (int i = 0; i < eventToCheck.GetPersistentEventCount(); i++)
        {
            if (eventToCheck.GetPersistentTarget(i) != null)
            {
                result = true;
            }
        }
        return result;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleDevMenu()
    {
        devMenu.SetActive(!devMenu.activeSelf);
    }
}
