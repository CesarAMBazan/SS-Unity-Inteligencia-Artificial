using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject destino;
    public Vector3 difference;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            var position = transform.position;
            var position1 = destino.transform.position;
            /* = new Vector3(position1.x - position.x,
                                             position1.y - position.y,
                                             position1.z - position.z); */
            var transformacionJugador = other.transform;
            var position2 = transformacionJugador.position;
            var distanciaEntreJugador = new Vector3(position2.x - position.x,
                position2.y, position2.z - position.z);
            //var transform1 = other.transform;
            //other.transform.position = position1 + new Vector3(0, transform1.position.y, 0);
            Debug.Log("Jugador= " + position2);
            Debug.Log("Destino= " + position1);
            Debug.Log("Actual= " + position);
            Debug.Log("Distancia= " + distanciaEntreJugador);
            // Debug.Log("JugadorRY= " + other.transform.rotation.y);
            //Debug.Log("DestinoRY= " + destino.transform.rotation.y) ;
            //Debug.Log("TriggerRY= " + transform.rotation.y);

            // Vector3 distanciaFinal = new Vector3(position1.x + distanciaEntreJugador.x, distanciaEntreJugador.y,
            // position1.z + distanciaEntreJugador.z);
            // Debug.Log("DistanciaFinal= " + distanciaFinal);
            other.transform.position = new Vector3(position1.x + distanciaEntreJugador.x, distanciaEntreJugador.y,
                position1.z + distanciaEntreJugador.z);
            /* var rotacionEntreJugador = new Quaternion(destino.transform.rotation.x,
                 destino.transform.rotation.y + (other.transform.rotation.y - transform.rotation.y), other.transform.rotation.z,
                 destino.transform.rotation.w);*/
            
            // Debug.Log("OperacionFinalRY= " + rotacionEntreJugador.y);
            //other.transform.rotation = rotacionEntreJugador;
            //other.transform.LookAt(destino.transform.position);
            // other.transform.Rotate(0f, -90f ,0.0f, Space.World);
        }
    }
}
