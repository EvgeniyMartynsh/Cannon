using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWidgetLifeBar : MonoBehaviour
{
    [SerializeField] private ProgressBarLife _progressBar;


    private void Start()
    {
        _progressBar.SetValue(GameManager.instance.healthNormolized);

        GameManager.instance.OnPlayerHealthChanged += HealthChanged;
    }

    public void HealthChanged(float newValueNormalized)
    {
        _progressBar.SetValue(newValueNormalized);
    }

    private void OnDisable()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.OnPlayerHealthChanged -= HealthChanged;
        }
        else
        {
            Debug.Log("gameManager is null");
        }
        

    }
}
