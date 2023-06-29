using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public bool allowLoading = true;

    /// <summary>
    ///     Método que se llama al colisionar con un objeto
    /// </summary>
    /// <param name="other">El objeto que colisiono con este objeto</param>
    private void OnTriggerEnter(Collider other)
    {
        // Sí el objeto se trata de un jugador
        if (other.CompareTag("Player"))
        {
            // Se desbloquea el gatillo
            Cursor.lockState = CursorLockMode.None;
            // Se comienza la rutina para cambiar de escena
            StartCoroutine(ChangeScene());
        }
    }

    /// <summary>
    ///     Rutina para cambiar de escena
    /// </summary>
    private IEnumerator ChangeScene()
    {
        // Se carga la escena de luces de manera asíncrona
        var asyncOperation = SceneManager.LoadSceneAsync("Scenes/Lights");
        asyncOperation.allowSceneActivation = false;
        // Se espera a que cargue la escena antes de intentar cargar la siguiente
        while (asyncOperation.progress < 0.9f) yield return null;

        while (!allowLoading) yield return null;

        // Se carga la escena "Bad Ending"
        asyncOperation.allowSceneActivation = true;
        SceneManager.LoadSceneAsync("Scenes/Bad Ending", LoadSceneMode.Additive);
    }
}