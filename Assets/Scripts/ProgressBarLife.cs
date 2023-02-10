using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarLife : MonoBehaviour
{
    [SerializeField] private Image _imgFiller;
    [SerializeField] private TextMeshProUGUI _textValue;

    public void SetValue(float valueNormolized)
    {
        _imgFiller.fillAmount = valueNormolized;

        var valuePersent = Mathf.RoundToInt(valueNormolized * 100f);
        _textValue.text = $"{valuePersent}%";

    }

}