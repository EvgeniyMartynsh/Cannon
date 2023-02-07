using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    UpdateUILayer _ui;
    private void Start()
    {
        _ui = FindObjectOfType<UpdateUILayer>();
    }

    public void SetHealth()
    {
        GameManager.KeyGameHealth += 1;
        GameManager.GameHealth = GameManager.SetHealth(GameManager.KeyGameHealth);

        GameManager.KeyHealthCost += 1;
        GameManager.HealthCost = GameManager.SetHealthCost(GameManager.KeyHealthCost);

        _ui.UpdateUI();
    }
}
