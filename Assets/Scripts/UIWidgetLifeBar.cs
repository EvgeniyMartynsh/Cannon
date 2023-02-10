using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWidgetLifeBar : MonoBehaviour
{
    [SerializeField] private ProgressBarLife _progressBar;
    GameManager gameManager;


    private void Start()
    {
        gameManager = GameManager.instance;
        _progressBar.SetValue(GameManager.instance.healthNormolized);

        gameManager.OnPlayerHealthValueChangedEvent += OnPlayerHealthValueChanged;
    }

    public void OnPlayerHealthValueChanged(float newValueNormalized)
    {
        _progressBar.SetValue(newValueNormalized);
    }

    private void OnDisable()
    {
        if (gameManager != null)
        {
            gameManager.OnPlayerHealthValueChangedEvent -= OnPlayerHealthValueChanged;
        }
        else
        {
            Debug.Log("gameManager is null");
        }
        

    }
}
