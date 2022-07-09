using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteScene1 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject.Find("===LEVEL===").SetActive(false);
        Destroy(this);
    }
    
}
