using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip[] bgm;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            NextBgm();
            audioSource = null;
        }
    }

    private void NextBgm()
    {
        audioSource.clip = bgm[1];
        audioSource.loop = true;
        audioSource.Play();
    }
}
