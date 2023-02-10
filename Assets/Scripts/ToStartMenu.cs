using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToStartMenu : MonoBehaviour
{
    
    public void GoToStartMenuButton()
    {
        UpdateExtraCoins("extraCoins", GameManager.instance.ExtraCoins);
        GameManager.instance.IsGameActive = false;
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
