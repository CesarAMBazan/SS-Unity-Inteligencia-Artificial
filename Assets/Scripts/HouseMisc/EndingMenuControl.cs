using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingMenuControl : MonoBehaviour
{
    /// <summary>
    ///     Se desbloquea el cursor del jugador al desbloquear la pantalla
    /// </summary>
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>
    ///     Método que se llama al presionar el botón de "Salir del Juego", se carga la escena del menú.
    /// </summary>
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Scenes/Menu");
    }
}