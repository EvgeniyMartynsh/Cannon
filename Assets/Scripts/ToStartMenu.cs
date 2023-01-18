using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToStartMenu : MonoBehaviour
{
    public void GoToStartMenu()
    {

        SaveExtraCoins();        
        SceneManager.LoadScene(0);

    }

     void SaveExtraCoins()
    {
        BinarySaveData binarySave = new BinarySaveData();
        GameData gameData = new GameData();
        gameData.extraCoins = GameManager.ExtraCoins;
        gameData.initPlayerHealth = GameManager.StartHealth;
        gameData.fireRange = GameManager.FireRange;
        gameData.startScore = GameManager.StartScore;

        binarySave.Save(gameData);
        
    }
}
