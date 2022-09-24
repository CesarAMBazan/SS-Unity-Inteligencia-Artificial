using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportMinusX : MonoBehaviour
{
    public GameObject destino;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerTransform = other.transform.position;
            var position = transform.position;

            var distanciaEntreJugador = new Vector3(playerTransform.x - position.x,
                playerTransform.y, playerTransform.z - position.z);

            var positionDestino = destino.transform.position;
            other.transform.position = new Vector3(positionDestino.x - distanciaEntreJugador.x, playerTransform.y,
                positionDestino.z - distanciaEntreJugador.z);

            other.transform.Rotate(0f, -180f, 0, Space.World);
        }
    }
}
