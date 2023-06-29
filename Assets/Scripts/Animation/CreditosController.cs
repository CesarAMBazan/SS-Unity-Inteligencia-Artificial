using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditosController : MonoBehaviour
{
    /* Atributos publicos de la clase */
    public float velocidadCreditos = 2f;
    public float velocidadAcelerada = 4f;

    private bool acelerarCreditos;

    /// <summary>
    ///     El método update se llama cada frame
    /// </summary>
    private void Update()
    {
        // Se determina la velocidad de los creditos
        var velocidadActual = acelerarCreditos ? velocidadAcelerada : velocidadCreditos;
        // Se mueven los creditos a esa velocidad
        transform.Translate(Vector3.up * velocidadActual * Time.deltaTime);

        // Sí el boton click se mantiene presionado
        if (Input.GetMouseButton(0))
            // Se acelerarán los creditos
            acelerarCreditos = true;
        else
            acelerarCreditos = false;

        // Sí los créditos pasan por encima de la posición de la camara
        if (transform.position.y >= Camera.main.orthographicSize)
            // Se cargará la siguiente escena
            CargarSiguienteEscena();
    }

    /// <summary>
    ///     Método que carga la escena del Final Bueno
    /// </summary>
    private void CargarSiguienteEscena()
    {
        SceneManager.LoadScene("GoodEnding");
    }
}