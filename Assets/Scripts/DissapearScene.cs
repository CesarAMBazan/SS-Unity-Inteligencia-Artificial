using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearScene : MonoBehaviour
{
    [SerializeField] private GameObject scene;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            scene.SetActive(false);
        }
    }
}
