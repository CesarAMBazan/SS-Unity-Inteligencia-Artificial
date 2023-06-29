
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;


public class AppearScene : MonoBehaviour
{
    [SerializeField] private GameObject[] scenes;

    public void Start()
    {
        

        foreach (var scene in scenes)
        {
            scene.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!scenes.Any())
        {
            return;
        }
        if (other.CompareTag("Player"))
        {
            foreach (var scene in scenes)
            {
                scene.SetActive(true);
            }
        }
    }
    
}
