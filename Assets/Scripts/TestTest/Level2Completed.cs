using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Completed : MonoBehaviour
{
    [SerializeField] private Door Door;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out FirstPersonController player))
        {
            int respuestas = other.GetComponent<FirstPersonController>().RespuestasNivel2;
            if (respuestas >= 3)
            {
                Door.Open(other.transform.position);
            }
        }
    }
}
