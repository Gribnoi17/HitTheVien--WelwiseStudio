using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighter : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleFire = null;

    public void PlayParticleFire()
    {
        _particleFire.Play();
    }

    public void StopParticleFire()
    {
        _particleFire.Stop();
    }

    public bool CheckParticlePlaying()
    {
        return _particleFire.isPlaying;
    }
}
