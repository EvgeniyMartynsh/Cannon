using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClosePopup : MonoBehaviour
{
    Animator _animator;

    [SerializeField] GameObject _popup;

    // Start is called before the first frame update
    void Start()
    {

        _animator = _popup.GetComponent<Animator>();
    }

    public void HidePopup()
    {
        _animator.SetTrigger("Hide");
        StartCoroutine(SetPopupInactive());

    }

    IEnumerator SetPopupInactive() 
    {
        yield return new WaitForSeconds(0.5f);

        _popup.SetActive(false);
    }






}
