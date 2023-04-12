using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance = null;
    private AudioSource audioSource;
    private bool isMuted;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        isMuted = false;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();  
    }

    public void MuteMusic()
    {
        if (isMuted)
        {
            audioSource.mute = false;
            isMuted = false;
        }
        else
        {
            audioSource.mute = true;
            isMuted = true;
        }
    }

}
