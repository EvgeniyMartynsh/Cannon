using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using static Parser;

public class GameManager : MonoBehaviour
{

    private static float _fireRange;
    private static int _startScore;
    private static int _extraCoins;


    public static int KeyBaseHealth { get; set; }
    public static int KeyGameHealth { get; set; }
    public static int KeyHealthCost { get; set; }
    public static int GameHealth { get; set; }
    public static int HealthCost { get; set; }




    public static float FireRange { get => _fireRange; set { _fireRange = value; } }
    public static int GameScore { get; set; }
    public static int StartScore { get => _startScore; set { _startScore = value; } }
    public static int ExtraCoins { get => _extraCoins; set { _extraCoins = value; } }
    public static bool IsGameOver { get; set; }
    public static int EnemyCount { get; set; } = 1;


    private void Awake()
    {
        CheckGameDataBinaryFile();
        Initialisation();
        
    }

    void Update()
    {
        GameOver();
    }

    private void CheckGameDataBinaryFile()
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
    public static void Initialisation()
    {
        IsGameOver = false;

        BinarySaveData binarySave = new BinarySaveData();
        GameData data = new GameData();
        data = binarySave.Load();


        KeyBaseHealth = data.keyBaseHealth;
        KeyGameHealth = KeyBaseHealth;

        KeyHealthCost = 1;

        GameHealth = SetHealth(KeyGameHealth);
        HealthCost = SetHealthCost(KeyHealthCost);


        FireRange = data.fireRange;
        StartScore = data.startScore;
        ExtraCoins = data.extraCoins;
        GameScore = StartScore;

        UpdateUILayer _ui;
        _ui = FindObjectOfType<UpdateUILayer>();
        _ui.UpdateUI();
    }

    public static int SetHealth(int a)
    {
        var _health = Resources.Load<HealthData>("HealthDicInfo");
        int value = 0;

        for (int i = 0; i < _health.dictionaryElements.Count; i++)
        {
            if (_health.dictionaryElements[i].key == a)
                value = _health.dictionaryElements[i].value;     
        }
        return value;
    }

    public static int SetHealthCost(int a)
    {
        var _health = Resources.Load<HealthCostData>("HealthCostDicInfo");
        int value = 0;

        for (int i = 0; i < _health.dictionaryElements.Count; i++)
        {
            if (_health.dictionaryElements[i].key == a)
                value = _health.dictionaryElements[i].value;
        }
        return value;
    }

    void GameOver()
    {
        if (GameHealth <= 0)
        {
            IsGameOver = true;
        }
    }

}
