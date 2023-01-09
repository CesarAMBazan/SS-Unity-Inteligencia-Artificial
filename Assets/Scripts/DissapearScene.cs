using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DissapearScene : MonoBehaviour
{
    [SerializeField] private GameObject[] scenes;
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
                scene.SetActive(false);
            }
        }
    }
}
