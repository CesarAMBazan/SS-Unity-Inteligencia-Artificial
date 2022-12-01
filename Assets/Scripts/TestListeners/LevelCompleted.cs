using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleted : MonoBehaviour
{
    [SerializeField] private Door door;
    [SerializeField] private int nivel;
    [SerializeField] private int respuestasNecesarias;
    [SerializeField] private GameObject LevelTrigger;
    private Level3 level3;


    private void Awake()
    {
        level3 = LevelTrigger.GetComponent<Level3>();
    }

    

    private void FixedUpdate()
    {
        int respuestas = level3.Respuesta(nivel);
        if (respuestas >= respuestasNecesarias)
        {
            door.Open(Vector3.forward);
            Destroy(this);
        }
    }
}
