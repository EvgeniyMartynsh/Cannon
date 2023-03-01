using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClosePopup : MonoBehaviour
{
    Animator _animator;
    Button _button;
    [SerializeField] GameObject _popup;

    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
        _animator = _popup.GetComponent<Animator>();
    }

    public void HidePopup()
    {
        _animator.SetTrigger("Hide");

    }






}
