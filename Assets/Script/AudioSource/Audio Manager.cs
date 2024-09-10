using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Import นี้สำหรับการใช้งาน Slider

public class AudioManager : MonoBehaviour
{
    
    [Header("----------------- Audio Source --------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("----------------- Audio Clip --------")]
    public AudioClip background;
    //public AudioClip death;
    public AudioClip shoot;
    public AudioClip End_Win_Player_Q_Enamy;
    public AudioClip End_Over_Enamy_Q_Player;
    public AudioClip Hp;
    public AudioClip paper;
    public AudioClip cat;
    
   
    
    private void Start()
    { 
        
        //musicSource.clip = background;
        //musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
        
    }
    
    public void PlayBackgroundMusic()
    {
        if (musicSource != null && background != null && !musicSource.isPlaying)
        {
            musicSource.clip = background;  // ตั้งค่า AudioClip ที่จะเล่น
            musicSource.Play();
        }
    }

    public void StopBackgroundMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    
}
