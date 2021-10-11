using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip Trial;
    public AudioClip Cornered;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
    }

    public void PlayTrial()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
        audioSource.clip = Trial;
        audioSource.Play();
    }

    public void PlayCornered()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
        audioSource.clip = Cornered;
        audioSource.Play();
    }
}
