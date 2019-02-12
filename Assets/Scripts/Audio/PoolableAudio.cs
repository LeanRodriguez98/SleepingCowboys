using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableAudio : MonoBehaviour {

    public AudioClip clip;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayOneShot()
    {
        audioSource.PlayOneShot(clip);
    }

    public void PlayOneShot(float time)
    {
        Invoke("PlayOneShot", time);
    }

    public float GetSoundDuration()
    {
        return clip.length;
    }
}
