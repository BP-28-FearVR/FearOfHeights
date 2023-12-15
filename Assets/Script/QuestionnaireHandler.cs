using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class QuestionnaireHandler : MonoBehaviour
{
    [TextArea(15, 20)]
    [SerializeField] private string[] textList;

    [SerializeField] private TextMeshProUGUI textObj;

    private int _postion = 0;

    void Start()
    {
        if (textList.Length > 0)
        {
            textObj.text = textList[0];
        }
        else
        {
            textObj.text = "";
            textList = new string[1];
            textList[0] = "";
        }

        _postion = 0;
    }

    public void next()
    {
        _postion++;

        if (_postion >= textList.Length)
        {
            _postion = textList.Length - 1;
        }

        textObj.text = textList[_postion];
    }

    public void prev()
    {
        _postion--;

        if (_postion < 0)
        {
            _postion = 0;
        }

        textObj.text = textList[_postion];
    }
}