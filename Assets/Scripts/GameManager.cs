using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using static Parser;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    public event Action<float> OnPlayerHealthValueChangedEvent;

    private float _fireRange;
    private int _startScore;
    private int _extraCoins;
    private int _restorHealthPoints = 1;

    public float healthNormolized => (float)CurrentGameHealth / UpgradeHealth;


    public int KeyGameHealth { get; set; }
    public int KeyHealthCost { get; set; }
    public int CurrentGameHealth { get; set; }
    public int UpgradeHealth { get; set; }
    public int UpgradeHealthCost { get; set; }




    public float FireRange { get => _fireRange; set { _fireRange = value; } }
    public int GameScore { get; set; }
    public int StartScore { get => _startScore; set { _startScore = value; } }
    public int ExtraCoins { get => _extraCoins; set { _extraCoins = value; } }
    public bool IsGameOver { get; set; }
    public bool IsGameActive { get; set; }
    public int EnemyCount { get; set; } = 1;


    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


        CheckGameDataBinaryFile();
        Initialisation();
        OnPlayerHealthValueChangedEvent?.Invoke(healthNormolized);
        //StartCoroutine(RestoreCurrentGameHealth());
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
    public void Initialisation()
    {
        IsGameOver = false;
        IsGameActive = true;

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

    public int SetHealth(int a)
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

    public int SetHealthCost(int a)
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

    public void ChangeHealth(int damage)
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
        //TODO: тут наверно надо на ивенты переписать

        if (CurrentGameHealth < UpgradeHealth && !IsGameOver)
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
