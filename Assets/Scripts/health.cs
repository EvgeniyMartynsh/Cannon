using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    UpdateUILayer _ui;
    ProgressBarLife _life;

    private void Start()
    {
        _ui = FindObjectOfType<UpdateUILayer>();
        _life = FindObjectOfType<ProgressBarLife>();
    }

    public void UpgradeHealthButton()
    {
        GameManager.KeyGameHealth += 1;
        GameManager.KeyHealthCost += 1;

        GameManager.UpgradeHealth = GameManager.SetHealth(GameManager.KeyGameHealth);

        AddNewPointsToGameHealth(GameManager.KeyGameHealth);


        _life.SetValue(GameManager.healthNormolized);

        
        GameManager.UpgradeHealthCost = GameManager.SetHealthCost(GameManager.KeyHealthCost);

        _ui.UpdateUI();
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

        GameManager.CurrentGameHealth += GameManager.UpgradeHealth - value;
    }
}
