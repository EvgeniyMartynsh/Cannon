using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateUpgradeButtons : MonoBehaviour
{
    //public static ActivateUpgradeButtons instance;
    
    Animator _animator;
    Button _button;

    private void OnEnable()
    {
        _button = GetComponent<Button>();
        _animator = GetComponent<Animator>();
    }

    public void CheckUpgradeCost()
    {
        if (GameManager.instance.GameScore >= GameManager.instance.UpgradeHealthCost && 
            !GameManager.instance.IsGameOver)
        {
            _button.interactable = true;
            _animator.SetTrigger("Normal");
        }
        else
        {
            _button.interactable = false;
            _animator.SetTrigger("Disable");
        }
    }
}

