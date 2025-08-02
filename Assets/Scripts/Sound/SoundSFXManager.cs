using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSFXManager : MonoBehaviour
{
    public AudioSource sfxSource;
    public AudioClip diceSound;
    public AudioClip movementSound;
    public static SoundSFXManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void PlayRollSound()
    {
        sfxSource.PlayOneShot(diceSound);
    }
    public void PlayMovementSound()
    {
        sfxSource.PlayOneShot(movementSound);
    }
}
