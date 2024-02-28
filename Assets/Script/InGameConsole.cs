using TMPro;
using UnityEngine;

public class ConsoleToGUI : MonoBehaviour
{
    static string myLog = "";
    private string output;
    private string stack;

    [Tooltip("The ConsoleGUI GameObject")]
    [SerializeField] private GameObject consoleGUI;

    void OnEnable()
    {
        Application.logMessageReceived += Log;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= Log;
    }

    public void Log(string logString, string stackTrace, LogType type)
    {
        output = logString;
        stack = stackTrace;
        myLog = output + "\n" + myLog;
        if (myLog.Length > 5000)
        {
            myLog = myLog.Substring(0, 4000);
        }
        consoleGUI.GetComponentInChildren<TextMeshProUGUI>().text = myLog;
    }

    public void ToggleConsole()
    {
        consoleGUI.SetActive(!consoleGUI.activeSelf);
        Debug.Log("Lol");
    }
}