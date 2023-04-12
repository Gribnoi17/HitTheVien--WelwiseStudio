using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class AudioLighter : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _spark;
    [SerializeField] private AudioClip _boiling;
    [SerializeField] private AudioClip _finishSound;
    private bool isSoundPlaying = false;



    public void PlayFinishSound()
    {
        StopSound();
        _audioSource.volume = 0.8f;
        _audioSource.PlayOneShot(_finishSound);
    }

    public void PlaySparkSound()
    {
        _audioSource.loop = false;
        _audioSource.volume = 1f;
        _audioSource.PlayOneShot(_spark);       
    }

    private IEnumerator WaitSoundTime(float seconds)
    {
        isSoundPlaying = true;
        yield return new WaitForSeconds(seconds);
        isSoundPlaying = false;
    }

    public void PlayBoilingSound()
    {
        if(isSoundPlaying == false)
        {
            _audioSource.volume = 1f;
            _audioSource.loop = true;
            _audioSource.PlayOneShot(_boiling);
            StartCoroutine(WaitSoundTime(_boiling.length));
        }

    }


    public void StopSound()
    {
        _audioSource.Stop();
        isSoundPlaying = false;
    }
}
