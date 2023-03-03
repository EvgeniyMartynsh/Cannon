using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    [SerializeField] private int _sceneIndex;

    public void LoadScene()
    {
        StartCoroutine(LoadScene(_sceneIndex));
    }

    IEnumerator LoadScene(int sceneIndex)
    {
        yield return new WaitForSeconds(0.3f);

        SceneManager.LoadScene(sceneIndex);
    }
    

}
