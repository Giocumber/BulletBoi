using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicManager : MonoBehaviour
{
    [Header("------------- Audio Source -------------")]
    public AudioSource BGMusicSource;

    [Header("------------- Audio Clip -------------")]
    public AudioClip BGMusic;

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
