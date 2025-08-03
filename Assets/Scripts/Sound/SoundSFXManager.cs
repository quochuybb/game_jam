using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSFXManager : MonoBehaviour
{
    public AudioSource sfxSource;
    public AudioClip diceSound;
    public AudioClip hitSound;
    public AudioClip movementSound;
    public AudioClip winningSound;
    public AudioClip loseSound;


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
    public void PlayHitSound()
    {
        sfxSource.PlayOneShot(hitSound);
    }
    public void PlayWinningSound()
    {
        sfxSource.PlayOneShot(winningSound);
    }

    public void PlayLoseSound()
    {
        sfxSource.PlayOneShot(loseSound);
    }
}
