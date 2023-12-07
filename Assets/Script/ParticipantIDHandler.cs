using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ParticipantIDHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] private TextMeshProUGUI outputText;

    [SerializeField] private int maxCharInText = 16;

    private static string participantID = null;

    public static string ParticipantID { get => participantID; }

    void Start()
    {
        textField.text = "";
    }

    public void NumberButtonPress(int number)
    {
        if (textField.text.Length + 1 <= maxCharInText)
        {
            textField.text += number.ToString();
        }
    }

    public void RemoveButtonPress()
    {
        if (!string.IsNullOrEmpty(textField.text))
        {
            textField.text = textField.text.Substring(0, textField.text.Length - 1);
        }
    }

    public void SubmitButtonPress()
    {
        if (!string.IsNullOrEmpty(textField.text))
        {
            participantID = textField.text;
            outputText.text = participantID;
        }
    }

    void Update()
    {
        
    }
}
