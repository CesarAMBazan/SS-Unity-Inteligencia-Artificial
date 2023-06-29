using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    /* Atributos de la clase */
    public bool IsOpen;
    [SerializeField] private bool IsRotatingDoor = true;
    [SerializeField] private float Speed = 1f;

    [Header("Rotation Configs")] [SerializeField]
    private float RotationAmount = 90f;

    [SerializeField] private float ForwardDirection;

    private Coroutine AnimationCoroutine;
    private Vector3 Forward;

    private Vector3 StartRotation;

    /// <summary>
    ///     Cuando el objeto sea invocado
    /// </summary>
    private void Awake()
    {
        // Se le asignará cuanto rotará
        StartRotation = transform.rotation.eulerAngles;
        // Se le asignará una dirección de rotación
        Forward = transform.right;
    }

    /// <summary>
    ///     Método que se llama para abrir la puerta
    /// </summary>
    /// <param name="UserPosition">Vector que corresponde a la posición del jugador cuando se invoca este método</param>
    public void Open(Vector3 UserPosition)
    {
        // Sí la puerta no esta abierta
        if (!IsOpen)
        {
            // Sí la animación esta corriendo la detendremos
            if (AnimationCoroutine != null) StopCoroutine(AnimationCoroutine);

            // Sí se trata de una puerta que rota
            if (IsRotatingDoor)
            {
                // Iniciaremos la rotación a partir de la posición del jugador
                var dot = Vector3.Dot(Forward, (UserPosition - transform.position).normalized);
                AnimationCoroutine = StartCoroutine(DoRotationOpen(dot));
            }
        }
    }

    /// <summary>
    ///     Rutina para abrir una puerta que rota
    /// </summary>
    /// <param name="ForwardAmount">Hacia donde va a rotar la puerta</param>
    /// <returns></returns>
    private IEnumerator DoRotationOpen(float ForwardAmount)
    {
        var startRotation = transform.rotation;
        Quaternion endRotation;

        // Dirección hacia donde se rota la puerta
        if (ForwardAmount >= ForwardDirection)
            endRotation = Quaternion.Euler(new Vector3(0, StartRotation.y - RotationAmount, 0));
        else
            endRotation = Quaternion.Euler(new Vector3(0, StartRotation.y + RotationAmount, 0));

        // Se levanta la bandera de que la puerta esta abierta
        IsOpen = true;
        float time = 0;
        // Se realiza la animación de la rotación de la puerta
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }

    /// <summary>
    ///     Método para cerrar la puerta
    /// </summary>
    public void Close()
    {
        // Sí la puerta ya esta abierta
        if (IsOpen)
        {
            // Sí la animación esta corriendo la detendremos
            if (AnimationCoroutine != null) StopCoroutine(AnimationCoroutine);

            // Se inicia la animación de cerrar la puerta
            if (IsRotatingDoor) AnimationCoroutine = StartCoroutine(DoRotationClose());
        }
    }

    /// <summary>
    ///     Rutina para cerrar una puerta que rota
    /// </summary>
    /// <returns></returns>
    private IEnumerator DoRotationClose()
    {
        var startRotation = transform.rotation;
        var endRotation = Quaternion.Euler(StartRotation);

        IsOpen = false;
        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime;
        }
    }
}