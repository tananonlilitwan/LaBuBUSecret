using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //[SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

   // public AudioClip background;
    //public AudioClip checkpoint;

    public AudioClip death;
    //public AudioClip Robotdeath;

    public AudioClip SFXSurce;

    private void Start()
    {
        //musicSource.clip = background;
       // musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
