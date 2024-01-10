using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestionnaireData
{
    //Where the data is acctually saved.
    private int[] _data = null;

    // Indexer Getter for internal data.
    // If index is out of range, the temps array will throw the exception.
    public int this[int index]
    {
        get => _data[index];
        set => _data[index] = value;
    }

    // Contructor for Data. Initialize with -1
    public QuestionnaireData(int size)
    {
        _data = new int[size];

        for (int i = 0; i < _data.Length; i++)
        {
            _data[i] = -1;
        }
    }

    public void SafeToFile() {
        string basePath = "/sdcard/Documents/";

        Scene currentScene = SceneManager.GetActiveScene();
        string participantId = ParticipantIDHandler.ParticipantID;
        
        string filename = "questionnaire_sc-" + currentScene.name + "_pt-" + participantId + ".csv";

        string fullFilePath = Path.Combine(basePath, filename);

        System.IO.File.WriteAllText(fullFilePath, "Test");
    }
}
