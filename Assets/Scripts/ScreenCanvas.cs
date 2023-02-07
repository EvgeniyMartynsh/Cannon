using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCanvas : MonoBehaviour
{
    [SerializeField] private Canvas _homeCanvas;
    [SerializeField] private Canvas _upgradeCanvas;
    [SerializeField] private Canvas _canvasN3;
    [SerializeField] private Canvas _canvasN4;
    [SerializeField] private Canvas _canvasN5;

    [SerializeField] private List<Canvas> _canvasList;


    private void Start()
    {
        AddCanvasToList();
        DisableAllCanvases();
        _homeCanvas.gameObject.SetActive(true);
    }

    public void ActivateHomeCanvas()
    {
        DisableAllCanvases();
        _homeCanvas.gameObject.SetActive(true);
    }
    public void ActivateUpgradeMenuCanvas()
    {
        DisableAllCanvases();
        _upgradeCanvas.gameObject.SetActive(true);
    }
    public void ActivateCanvasN3()
    {
        DisableAllCanvases();
        _canvasN3.gameObject.SetActive(true);
    }
    public void ActivateCanvasN4()
    {
        DisableAllCanvases();
        _canvasN4.gameObject.SetActive(true);
    }

    public void ActivateCanvasN5()
    {
        DisableAllCanvases();
        _canvasN5.gameObject.SetActive(true);
    }



    void DisableAllCanvases()
    {
        foreach (var item  in _canvasList)
        {
            item.gameObject.SetActive(false);
        }
    }

    void AddCanvasToList()
    {
        _canvasList.Add(_homeCanvas);
        _canvasList.Add(_upgradeCanvas);
        _canvasList.Add(_canvasN3);
        _canvasList.Add(_canvasN4);
        _canvasList.Add(_canvasN5);
    }


}
