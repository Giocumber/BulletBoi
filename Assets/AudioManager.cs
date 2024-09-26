using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------------- Audio Source -------------")]
    public AudioSource musicSource;
    public AudioSource SFXSource;
    public AudioSource SFXBiteSource;
    public AudioSource SFXHeartbeatSource;


    [Header("------------- Audio Clip -------------")]
    public AudioClip gunShotSfx;
    public AudioClip biteSfx;
    public AudioClip heartbeatSfx;

    public void PlaySFX(AudioClip sfxName)
    {
        SFXSource.clip = sfxName;
        SFXSource.Play();
    }

    public void PlayBiteSFX()
    {
        SFXBiteSource.clip = biteSfx;
        SFXBiteSource.Play();
    }

    public void HeartbeatSFX()
    {
        SFXHeartbeatSource.clip = heartbeatSfx;
        SFXHeartbeatSource.Play();
    }
}
