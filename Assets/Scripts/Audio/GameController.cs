using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public AudioClip gameMusic;
    // Start is called before the first frame update
    private void Awake()
    {
        MusicManager musicManager = FindObjectOfType<MusicManager>();
        musicManager.ChangeAudioClip(gameMusic);
    }
}
