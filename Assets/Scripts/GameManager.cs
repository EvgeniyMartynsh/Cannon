using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using static Parser;

public class GameManager : MonoBehaviour
{
    public static event Action<float> OnPlayerHealthValueChangedEvent;

    private static float _fireRange;
    private static int _startScore;
    private static int _extraCoins;
    private static int _restorHealthPoints = 1;

    public static float healthNormolized => (float)CurrentGameHealth / UpgradeHealth;


    public static int KeyGameHealth { get; set; }
    public static int KeyHealthCost { get; set; }
    public static int CurrentGameHealth { get; set; }
    public static int UpgradeHealth { get; set; }
    public static int UpgradeHealthCost { get; set; }




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
        OnPlayerHealthValueChangedEvent?.Invoke(healthNormolized);
        StartCoroutine(RestoreCurrentGameHealth());
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



        KeyGameHealth = data.keyBaseHealth;

        KeyHealthCost = 1;

        CurrentGameHealth = SetHealth(KeyGameHealth); ;
        UpgradeHealth = CurrentGameHealth;

        UpgradeHealthCost = SetHealthCost(KeyHealthCost);

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

    public static void ChangeHealth(int damage)
    {
        CurrentGameHealth -= damage;
        OnPlayerHealthValueChangedEvent?.Invoke(healthNormolized);
    }

    IEnumerator RestoreCurrentGameHealth()
    {
        Debug.Log("Ienumerator");

        yield return new WaitForSeconds(1);


        UpdateUILayer _ui;
        _ui = FindObjectOfType<UpdateUILayer>();

        ProgressBarLife _life;
        _life = FindObjectOfType<ProgressBarLife>();

        if (CurrentGameHealth < UpgradeHealth)
        {
            CurrentGameHealth += _restorHealthPoints;
            _ui.UpdateUI();
            _life.SetValue(healthNormolized);
        }

        StartCoroutine(RestoreCurrentGameHealth());

        //while (!IsGameOver)
        //{
        //    RestoreCurrentGameHealth();
        //}
    }

    void GameOver()
    {
        if (CurrentGameHealth <= 0)
        {
            IsGameOver = true;
        }
    }

}
