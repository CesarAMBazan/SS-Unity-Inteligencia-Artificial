using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportZ : MonoBehaviour
{
    public GameObject destino;

    /// <summary>
    /// Método que se llama cuando el jugador entra a la colisión de este objeto
    /// </summary>
    /// <param name="other">La colisión del objeto que interactúo con este objeto</param>
    private void OnTriggerEnter(Collider other)
    {
        // Sí el objeto del objeto de la colisión es el jugador
        if (other.CompareTag("Player"))
        {
            // Se obtiene la posición del jugador
            var playerTransform = other.transform.position;
            var position = transform.position;
            
            // Se obtiene la distancia entre el jugador y este objeto
            var distanciaEntreJugador = new Vector3(playerTransform.x - position.x,
                playerTransform.y, playerTransform.z - position.z);

            // Se obtiene la posición destino
            var positionDestino = destino.transform.position;
            
            // Se mueve al jugador a la misma posición del destino relativo a donde se encontraba el jugador primero
            other.transform.position = new Vector3(positionDestino.x - distanciaEntreJugador.z, playerTransform.y,
                positionDestino.z + distanciaEntreJugador.x);
            
            // Se rota al jugador -90 grados
            other.transform.Rotate(0f, -90, 0, Space.World);
        }
    }
}
