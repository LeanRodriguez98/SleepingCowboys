using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableAudio : MonoBehaviour {
    [Header("Clip to pool")]
    public AudioClip clip;
    [Header("The audiosource component of this object")]
    public AudioSource audioSource;

    public void Start()
    {
        SetAudioData();
    }

    public void SetAudioData()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.loop = false;
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0.5f;
        audioSource.volume = 1;
    }

    public void Play()
    {
        audioSource.Play();
    }

    public void Play(float time)
    {
        Invoke("Play", time);
    }

    public float GetSoundDuration()
    {
        return clip.length;
    }
}
