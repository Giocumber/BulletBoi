using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicManager : MonoBehaviour
{
    private static BGMusicManager instance;

    [Header("------------- Audio Source -------------")]
    public AudioSource BGMusicSource;

    [Header("------------- Audio Clip -------------")]
    public AudioClip BGMusic;

    void Awake()
    {
        // Check if an instance of AudioManager already exists
        if (instance == null)
        {
            instance = this; // If not, set this instance
            DontDestroyOnLoad(gameObject); // Make this instance persist across scenes
        }
        else
        {
            Destroy(gameObject); // If an instance already exists, destroy the duplicate
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayBackgroundMusic();
        DontDestroyOnLoad(gameObject);
    }

    public void PlayBackgroundMusic()
    {
        Debug.Log("msuicplay");
        BGMusicSource.clip = BGMusic;
        BGMusicSource.Play();
    }

    
}
