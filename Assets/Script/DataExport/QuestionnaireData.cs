using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class QuestionnaireData
{
    //Where the data is acctually saved.
    private int[] _data = null;

    // Indexer declaration.
    // If index is out of range, the temps array will throw the exception.
    public int this[int index]
    {
        get => _data[index];
        set => _data[index] = value;
    }

    public QuestionnaireData(int size)
    {
        _data = new int[size];
    }

    public void SafeToFile() {
        string basePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

        Scene currentScene = SceneManager.GetActiveScene();
        string participantId = ParticipantIDHandler.ParticipantID;
        
        string filename = "questionnaire_sc-" + currentScene.name + "_pt-" + participantId + ".csv";

        string fullFilePath = Path.Combine(basePath, filename);

        System.IO.File.WriteAllText(fullFilePath, "Test");
    }
}
