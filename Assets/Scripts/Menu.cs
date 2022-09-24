using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject menu;

    public GameObject loadingInterface;

    public Image loadingProgressBar;

    private List<AsyncOperation> scenesToLoad = new();


    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void StartGame()
    {
        
        HideMenu();
        ShowLoadingScreen();
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Scenes/Lights"));
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Scenes/Level1", LoadSceneMode.Additive));
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Scenes/Level2", LoadSceneMode.Additive));
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Scenes/Level3", LoadSceneMode.Additive));
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Scenes/Gameplay", LoadSceneMode.Additive));
        StartCoroutine(LoadingScreen());
    }

    private void HideMenu()
    {
        menu.SetActive(false);
    }

    private void ShowLoadingScreen()
    {
        loadingInterface.SetActive(true);
    }

    private IEnumerator LoadingScreen()
    {
        float totalProgress = 0;
        
        foreach (var t in scenesToLoad)
        {
            while (!t.isDone)
            {
                totalProgress += t.progress;
                loadingProgressBar.fillAmount = totalProgress / scenesToLoad.Count;
                yield return null;
            }
            
        }

        
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

