using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportZ : MonoBehaviour
{
    public GameObject destino;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerTransform = other.transform.position;
            var position = transform.position;
            
            var distanciaEntreJugador = new Vector3(playerTransform.x - position.x,
                playerTransform.y, playerTransform.z - position.z);

            var positionDestino = destino.transform.position;
            other.transform.position = new Vector3(positionDestino.x - distanciaEntreJugador.z, playerTransform.y,
                positionDestino.z + distanciaEntreJugador.x);
            
            other.transform.Rotate(0f, -90f, 0, Space.World);
        }
    }
}
