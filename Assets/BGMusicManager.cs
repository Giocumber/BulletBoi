using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicManager : MonoBehaviour
{
    private static BGMusicManager instance;

    [Header("------------- Audio Source -------------")]
    public AudioSource BGMusicSource1;
    public AudioSource BGMusicSource2;

    [Header("------------- Audio Clip -------------")]
    public AudioClip BGMusic1;
    public AudioClip BGMusic2;

    private bool gameIsOver = false;

    void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (instance == null)
        {
            instance = this; // Set this instance
            DontDestroyOnLoad(gameObject); // Make this instance persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy the duplicate
        }
    }

    void Start()
    {
        // Start playing the first background music
        PlayBackgroundMusic(BGMusicSource1, BGMusic1);
        StartCoroutine(BackgroundMusicLoop());
    }

    IEnumerator BackgroundMusicLoop()
    {
        while (!gameIsOver) // Continue until the game is over
        {
            if (!BGMusicSource1.isPlaying && !BGMusicSource2.isPlaying)
            {
                // Swap the music once one track finishes
                if (BGMusicSource1.clip == BGMusic1)
                {
                    PlayBackgroundMusic(BGMusicSource2, BGMusic2);
                }
                else
                {
                    PlayBackgroundMusic(BGMusicSource1, BGMusic1);
                }
            }
            yield return null; // Wait until the next frame to check again
        }
    }

    public void PlayBackgroundMusic(AudioSource audioSource, AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    // This function can be called when the game is over
    public void StopMusic()
    {
        gameIsOver = true; // Stops the background music loop
        BGMusicSource1.Stop();
        BGMusicSource2.Stop();
    }
}
