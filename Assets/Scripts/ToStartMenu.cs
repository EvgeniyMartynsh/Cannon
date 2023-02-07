using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToStartMenu : MonoBehaviour
{
    public void GoToStartMenuButton()
    {

        UpdateExtraCoins("extraCoins", GameManager.ExtraCoins);
        SceneManager.LoadScene(0);
    }


    public void UpdateExtraCoins(string fieldName, object newValue)
    {
        BinarySaveData binarySaveData = new BinarySaveData();
        GameData gameData = new GameData();
        gameData = binarySaveData.Load();

        Type type = gameData.GetType();
        FieldInfo fieldInfo = type.GetField(fieldName);
        fieldInfo.SetValue(gameData, newValue);

        binarySaveData.Save(gameData);
    }

}
