using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    UpdateUILayer _ui;
    ProgressBarLife _life;
    GameManager gameManager;

    private void Start()
    {
        _ui = FindObjectOfType<UpdateUILayer>();
        _life = FindObjectOfType<ProgressBarLife>();
        gameManager = GameManager.instance;
    }

    public void UpgradeHealthButton()
    {
        if (!gameManager.IsGameOver)
        {
            gameManager.KeyGameHealth += 1;
            gameManager.KeyHealthCost += 1;

            gameManager.UpgradeHealth = gameManager.SetHealth(gameManager.KeyGameHealth);
            AddNewPointsToGameHealth(gameManager.KeyGameHealth);

            GameManager.instance.DeductGameScorePoints();

            _life.SetValue(gameManager.healthNormolized);


            gameManager.UpgradeHealthCost = gameManager.SetHealthCost(gameManager.KeyHealthCost);

        }
    }

    private void AddNewPointsToGameHealth(int keyGameHealth)
    {
        var _health = Resources.Load<HealthData>("HealthDicInfo");
        int value = 0;
        int previousKeyGameHealth = keyGameHealth - 1;

        for (int i = 0; i < _health.dictionaryElements.Count; i++)
        {
            if (_health.dictionaryElements[i].key == previousKeyGameHealth)
                value = _health.dictionaryElements[i].value;
        }

        gameManager.CurrentGameHealth += gameManager.UpgradeHealth - value;
    }
}
