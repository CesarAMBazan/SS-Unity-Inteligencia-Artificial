using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseListener : MonoBehaviour
{
    private GameObject ChineseListener;

    private void Awake()
    {
        ChineseListener = GameObject.Find("ChineseBoxListener");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            ChineseListener.GetComponent<BoxListener>().CompareBoxes(other.gameObject);
        }
    }
}
