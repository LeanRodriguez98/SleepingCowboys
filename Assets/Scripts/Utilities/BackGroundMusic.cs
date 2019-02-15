using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour {

    private static BackGroundMusic instance;
    private AudioSource audioSource;
    public AudioClip audioClip;
    [Range(0.0f,1.0f)] public float volume;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = this;
        else
            gameObject.SetActive(false);
    }
    void Start () {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.loop = true;
        audioSource.volume = volume;
        audioSource.spatialBlend = 0;
        audioSource.Play();
    }
	
	
}
