using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static Parser;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public event Action<float> OnPlayerHealthChanged;
    public event Action OnUIValueChanged;   

    private float _fireRange;
    private int _startScore;
    private int _extraCoins;
    private int _restorHealthPoints = 1; //TODO: временное решение

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
    public int EnemyCount { get; set; } = 1;

    private ActivateUpgradeButtons _activateUpgradeButtons;
    

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

       // StartCoroutine(RestoreCurrentGameHealth());
        _activateUpgradeButtons = GameObject.Find("HealthUpgradeButton").GetComponent<ActivateUpgradeButtons>();

    }
    private void Start()
    {
        OnPlayerHealthChanged?.Invoke(healthNormolized);
        OnUIValueChanged?.Invoke();
    }
    private void CheckGameDataBinaryFile()
    {
        if (!File.Exists(Application.persistentDataPath + "/gameData.dat"))
        {
            BinarySaveData binaryData = new BinarySaveData();
            binaryData.SaveFromScriptableobject();
        }

        if (File.Exists(Application.persistentDataPath + "/gameData.dat"))
        {
            Debug.Log("File Found");
        }
    }
    public void Initialisation()
    {
        Debug.Log("init");
        IsGameOver = false;

        BinarySaveData binarySave = new BinarySaveData();
        GameData data = new GameData();
        data = binarySave.Load();

        KeyGameHealth = data.keyBaseHealth;

        KeyHealthCost = 1;

        CurrentGameHealth = SetHealth(KeyGameHealth);
        UpgradeHealth = CurrentGameHealth;

        UpgradeHealthCost = SetHealthCost(KeyHealthCost);

        FireRange = data.fireRange;
        StartScore = data.startScore;
        ExtraCoins = data.extraCoins;
        GameScore = StartScore;

        _activateUpgradeButtons = GameObject.Find("HealthUpgradeButton").GetComponent<ActivateUpgradeButtons>();
        _activateUpgradeButtons.CheckUpgradeCost();

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

    public void ChangeHealthPoints(int damage)
    {

        if (CurrentGameHealth - damage > 0)
        {
            CurrentGameHealth -= damage;
        }

        else
        {
            CurrentGameHealth = 0;
            GameOver();
        }

        OnPlayerHealthChanged?.Invoke(healthNormolized);
        OnUIValueChanged?.Invoke();
    }

    public void AddGameScorePoints(int addPoints)
    {
        GameScore += addPoints;
        _activateUpgradeButtons.CheckUpgradeCost();
        OnUIValueChanged.Invoke();

    }

    public void DeductGameScorePoints()
    {
        GameScore -= UpgradeHealthCost; 
        _activateUpgradeButtons.CheckUpgradeCost();
        OnUIValueChanged.Invoke();
    }

    public void ChangeExtraCoins(int addExtraCoins)
    {
        ExtraCoins += addExtraCoins;
        OnUIValueChanged.Invoke();
    }

    IEnumerator RestoreCurrentGameHealth()
    {
        yield return new WaitForSeconds(1);

        if (CurrentGameHealth < UpgradeHealth && !IsGameOver)
        {
            CurrentGameHealth += _restorHealthPoints;

            OnPlayerHealthChanged?.Invoke(healthNormolized);
            OnUIValueChanged.Invoke();
        }

        if (!IsGameOver)
        {
            StartCoroutine(RestoreCurrentGameHealth());
        }
    }

    void GameOver()
    {
        IsGameOver = true;

        PopupListInGameScene popupListInGameScene = GameObject.Find("Canvas").
            GetComponent<PopupListInGameScene>();

        popupListInGameScene.ActivateGameOverPopup();

    }
}
