using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerExit : MonoBehaviour
{
    [SerializeField] private Door Door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out FirstPersonController player))
        {
            if (Door.IsOpen)
            {
                Door.Close();
            }
        }
    }
}
