using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoveBoxes : MonoBehaviour
{
    public GameObject boxesParent;
    public GameObject spawnPoint;
    public GameObject listener;
    public List<GameObject> eachbox;
    
    public bool enter = false;

    private void Awake()
    {
        listener = GameObject.Find("ChineseBoxListener");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            listener.GetComponent<BoxListener>().SelectObjective();
            Destroy(this);
        }
        
    }
}
