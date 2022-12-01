using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Level3 : MonoBehaviour
{
    private FirstPersonController firstPersonController;
    private int respuesta;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            firstPersonController = GameObject.Find("Player").GetComponent<FirstPersonController>();
        }
    }
    

    public void respuestaCorrecta(int lvl)
    {
        firstPersonController.RespuestaCorrecta(lvl);
    }

    public void resetRespuesta(int lvl)
    {
        firstPersonController.ResetRespuesta(lvl);
    }

    public int Respuesta(int lvl) => firstPersonController.RespuestasNivel(lvl);
    
}
