using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public bool allowLoading = true;


    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Cursor.lockState = CursorLockMode.None;
            StartCoroutine(ChangeScene());
        }
    }

    private IEnumerator ChangeScene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Scenes/Lights");
        asyncOperation.allowSceneActivation = false;
        while (asyncOperation.progress < 0.9f)
        {
            yield return null;
        }

        while (!allowLoading)
        {
            yield return null;
        }

        asyncOperation.allowSceneActivation = true;
        SceneManager.LoadSceneAsync("Scenes/Bad Ending", LoadSceneMode.Additive);
    }
}
