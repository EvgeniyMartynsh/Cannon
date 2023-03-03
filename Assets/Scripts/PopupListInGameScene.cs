using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupListInGameScene : MonoBehaviour
{
    [SerializeField] private List<GameObject> _popupList;

    [SerializeField] private GameObject _gameOverPopup;
    [SerializeField] private GameObject _menuPopup;



    private void Start()
    {
        AddPopupToList();
        DisableAllPopups();
    }

    public void ActivateGameOverPopup()
    {
        DisableAllPopups();
        _gameOverPopup.gameObject.SetActive(true);
    }

    public void ActivateMenuPopup()
    {
        DisableAllPopups();
        _menuPopup.gameObject.SetActive(true);
    }

    void DisableAllPopups()
    {
        foreach (var item in _popupList)
        {
            item.gameObject.SetActive(false);
        }
    }

    void AddPopupToList()
    {
        _popupList.Add(_gameOverPopup);
        _popupList.Add(_menuPopup);
    }
    


}
