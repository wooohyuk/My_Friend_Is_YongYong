using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundclip_Changer : MonoBehaviour
{

    public AudioSource audioSrc;

    public AudioClip clip_voice;
    public AudioClip clip_eat;
    public AudioClip clip_cre;
    public AudioClip clip_tou;
    public AudioClip clip_die;

    public static int hotsix = 0;

    public void Voice()
    {
        audioSrc.clip = null;
        hotsix = 1;
    }

    public void Eat()
    {
        audioSrc.clip = null;
        hotsix = 2;
    }

    public void Cre_Heart()
    {
        audioSrc.clip = null;
        hotsix = 3;
    }

    public void Tou_Heart()
    {
        audioSrc.clip = null;
        hotsix = 4;
    }
    public void Die()
    {
        audioSrc.clip = null;
        hotsix = 5;
    }
    int bc = 1;

   
    void Start()
    {
        hotsix = 0;
        audioSrc = GetComponent<AudioSource>();
    }

    void Update()
    {
       
        if (hotsix == 1)
        {
            if (audioSrc.clip != clip_voice)
            {
                bc = 1;
                if (bc == 1)
                {
                    audioSrc.clip = (clip_voice);
                    audioSrc.Play();
                    bc = 0;
                }
            }
        }
        if (hotsix == 2)
        {
            if (audioSrc.clip != clip_eat)
            {
                bc = 1;
                if (bc == 1)
                {
                    audioSrc.clip = (clip_eat);
                    audioSrc.Play();
                    bc = 0;
                }
            }
        }
        if (hotsix == 3)
        {
            if (audioSrc.clip != clip_cre)
            {
                bc = 1;
                if (bc == 1)
                {
                    audioSrc.clip = (clip_cre);
                    audioSrc.Play();
                    bc = 0;
                }
            }
        }
        if (hotsix == 4)
        {
            if (audioSrc.clip != clip_tou)
            {
                bc = 1;
                if (bc == 1)
                {
                    audioSrc.clip = (clip_tou);
                    audioSrc.Play();
                    bc = 0;
                }
            }
        }
        if (hotsix == 5)
        {
            if (audioSrc.clip != clip_die)
            {
                bc = 1;
                if (bc == 1)
                {
                    audioSrc.clip = (clip_die);
                    audioSrc.Play();
                    bc = 0;
                }
            }
        }
    }
}
