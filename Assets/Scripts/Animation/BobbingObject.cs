using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BobbingObject : MonoBehaviour
{
    [SerializeField] private float ampliacion = 0.2f;
    [SerializeField] private float velocidad = 2f;
    private Vector3 spawnPoint;

    private void Start()
    {
        spawnPoint = transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(spawnPoint.x, spawnPoint.y + Mathf.Sin(Time.time * velocidad) * ampliacion, spawnPoint.z);
    }
}
