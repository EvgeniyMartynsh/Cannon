using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateUILayer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI extraCoinsText;
    [SerializeField] TextMeshProUGUI fireRangeText;
    [SerializeField] TextMeshProUGUI rotationSpeedText;
    [SerializeField] TextMeshProUGUI bulletSpeedText;
    [SerializeField] TextMeshProUGUI healthPlayerText;
    [SerializeField] TextMeshProUGUI healthCostText;
    [SerializeField] TextMeshProUGUI currentHealth;

    //TODO: данный скрипт висит на камере, а ему там вообще надо висеть?


    private void OnEnable()
    {
        GameManager.instance.OnUIValueChanged += UpdateUI;
    }

    private void OnDisable()
    {
        GameManager.instance.OnUIValueChanged -= UpdateUI;
    }

    public void UpdateUI()
    {
            scoreText.text = "$ " + GameManager.instance.GameScore;
            extraCoinsText.text = "$ extra " + GameManager.instance.ExtraCoins;
            fireRangeText.text = "Range " + GameManager.instance.FireRange;
            rotationSpeedText.text = "Rotation speed:  " + Cannon.RotationSpeed;
            bulletSpeedText.text = "Bullet speed: " + Projectile.Speed;
            healthPlayerText.text = Convert.ToString(GameManager.instance.UpgradeHealth);
            healthCostText.text = "$ " + Convert.ToString(GameManager.instance.UpgradeHealthCost);
            currentHealth.text = "H: " + GameManager.instance.CurrentGameHealth;
    }

}
