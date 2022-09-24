
using System;
using UnityEngine;
using UnityEngine.Serialization;


public class AppearScene : MonoBehaviour
{
    [SerializeField] private GameObject scene;

    public void Start()
    {
        scene.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            scene.SetActive(true);
        }
    }
}
