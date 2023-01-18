using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Battle : MonoBehaviour
{


    public void StartBattle()
    {
        SceneManager.LoadScene(1);
        InitSaveFiles();
        GameManager.Initialisation();

    }

    void InitSaveFiles()
    {
        if (!File.Exists(Application.persistentDataPath + "/gameData.dat"))
        {
            BinarySaveData binaryData = new BinarySaveData();
            binaryData.SaveFromScriptableobject();
            Debug.Log("SaveFromSO - ok");
        }
        if (File.Exists(Application.persistentDataPath + "/gameData.dat"))
        {
            Debug.Log("File Found");

        }
    }

}
