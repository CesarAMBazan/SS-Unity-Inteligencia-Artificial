using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public AudioClip creditsSong;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Cursor.lockState = CursorLockMode.None;
            ChangeScene();
        }
    }

    private void ChangeScene()
    {
        MusicManager musicManager = FindObjectOfType<MusicManager>();
        musicManager.ChangeAudioClip(creditsSong);
        SceneManager.LoadScene("Creditos");
    }
}
