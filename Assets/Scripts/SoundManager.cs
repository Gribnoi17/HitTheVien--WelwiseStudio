using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private BackgroundMusic backgroundMusic;

    private void Start()
    {
        backgroundMusic = FindObjectOfType<BackgroundMusic>();
    }

    public void MuteBackgoroundMusic()
    {
        backgroundMusic.MuteMusic();
    }
}
