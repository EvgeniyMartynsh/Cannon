using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveECoinsAndDestroyGM : MonoBehaviour
{
    
    public void SaveExtraCoinsAndDestroyGM()
    {
        UpdateExtraCoins("extraCoins", GameManager.instance.ExtraCoins);
        Destroy(GameManager.instance);

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
