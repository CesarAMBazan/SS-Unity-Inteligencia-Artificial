using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Atributos de la clase PauseManu
    public GameObject pauseMenu;

    // Booleano que indica si esta en pausa el juego o no
    public static bool isPaused;

    /// <summary>
    /// El método Start corre automaticamente al crear el GameObject al que este componente esta enlazado.
    /// </summary>
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    /// <summary>
    /// Método Update que se llama cada Frame
    /// </summary>
    void Update()
    {
        // Sí el jugador presiona la tecla P.
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Y el juego ya esta pausado
            if (isPaused)
            {
                // Se llama al método para reanudar el juego
                ResumeGame();
            }
            else PauseGame(); // Sino se pausa el juego-
        }
    }


    /// <summary>
    /// Método que pausa el juego al ser llamado
    /// </summary>
    private void PauseGame()
    {
        // Se activa el menú de pausa en la UI
        pauseMenu.SetActive(true);
        // Se detienen las actualizaciones de tiempo en el juego
        Time.timeScale = 0f;
        // Se indica que el juego se encuentra pausado
        isPaused = true;
        // Se desbloquea el cursor del mouse
        Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>
    /// Método para reanudar el juego
    /// </summary>
    public void ResumeGame()
    {
        // Se desactiva el menú de pausa en la UI
        pauseMenu.SetActive(false);
        // Vuelven a la normalidad las actualizaciones de tiempo en el juego
        Time.timeScale = 1f;
        // Se indica que el juego no esta pausado
        isPaused = false;
        // Se bloquea el cursor del mouse
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Método que al ser llamado carga la escena de Menú
    /// </summary>
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.None;
        // Con el SceneManager se carga la escena con nombre 'Menu'
        SceneManager.LoadScene("Scenes/Menu");
    }
}