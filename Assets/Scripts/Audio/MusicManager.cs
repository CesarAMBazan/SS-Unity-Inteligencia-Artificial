using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeAudioClip(AudioClip newClip)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = newClip;
        audioSource.Play();
    }
}
