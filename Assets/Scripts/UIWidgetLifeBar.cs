using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWidgetLifeBar : MonoBehaviour
{
    [SerializeField] private ProgressBarLife _progressBar;

    private void OnEnable()
    {
        _progressBar.SetValue(GameManager.healthNormolized);

        GameManager.OnPlayerHealthValueChangedEvent += OnPlayerHealthValueChanged;
    }

    public void OnPlayerHealthValueChanged(float newValueNormalized)
    {
        _progressBar.SetValue(newValueNormalized);
    }

    private void OnDisable()
    {
        GameManager.OnPlayerHealthValueChangedEvent -= OnPlayerHealthValueChanged;

    }
}
