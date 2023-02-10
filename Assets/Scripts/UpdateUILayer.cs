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
    
    public void UpdateUI()
    {
        scoreText.text = "$ " + GameManager.GameScore;
        extraCoinsText.text = "$ extra " + GameManager.ExtraCoins;
        fireRangeText.text = "Range " + GameManager.FireRange;
        rotationSpeedText.text = "Rotation speed:  " + Cannon.RotationSpeed;
        bulletSpeedText.text = "Bullet speed: " + Projectile.Speed;
        healthPlayerText.text = Convert.ToString(GameManager.UpgradeHealth);
        healthCostText.text = "$ " + Convert.ToString(GameManager.UpgradeHealthCost);
        currentHealth.text = "H: " + GameManager.CurrentGameHealth;
    }

}
