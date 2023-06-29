using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteScene1 : MonoBehaviour
{
    // public SceneAsset sceneToDestroy;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.UnloadSceneAsync("Scenes/Level1");
        }
        //GameObject.Find("===LEVEL===").SetActive(false);
        //Destroy(this);
    }
    
}
