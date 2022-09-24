using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeleteLevel2 : MonoBehaviour
{
    // public SceneAsset sceneToDestroy;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            SceneManager.UnloadSceneAsync("Scenes/Level2");
        }
        //GameObject.Find("===LEVEL===").SetActive(false);
        //Destroy(this);
    }
    
}
