using UnityEngine;

/* 
 * This script invokes the questionnaire UI after a given time x
 * The timer starts when the scene is loaded
 */

public class QuestionnaireInvoker : MonoBehaviour
{
    // Time to wait after scene has started
    [SerializeField] private float secondsToWait = 20.0f;

    // Add the questionnaire object to the according field of the questionnaire trigger object in the unity editor
    [SerializeField] private GameObject questionnaire;
    // Add the relaxation UI object to the according field of the questionnaire trigger object in the unity editor
    [Tooltip("Add the relaxation UI object of this scene which shall be deactivated before showing the questionnaire")]
    [SerializeField] private GameObject relaxUI;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("TriggerQuestionnaire", secondsToWait);
    }

    // this method deactivates the relaxation UI and enables the questionnaire
    public void TriggerQuestionnaire()
    {
        relaxUI.SetActive(false);
        questionnaire.SetActive(true);
    }

    // For use by the dev menu
    public void setTimeByDevMenu(float newSecondsToWait)
    {
        secondsToWait = newSecondsToWait;
        CancelInvoke("TriggerQuestionnaire");
        Invoke("TriggerQuestionnaire", secondsToWait);
    }
}
