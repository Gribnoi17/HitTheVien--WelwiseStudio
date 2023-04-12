using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    private Dragging _drag;
    private Lighter _lighter;
    private Spoon _spoon;


    private void Start()
    {
        _drag = FindObjectOfType<Dragging>();
        _spoon= FindObjectOfType<Spoon>();
        _lighter = FindObjectOfType<Lighter>();

    }

    // Update is called once per frame
    private void Update()
    {
        PlayLighterParticle();
        PlaySpoonParticle();
    }



    private void PlayLighterParticle()
    {
        if (_drag.GetIsDragging())
        {
            if (!_lighter.CheckParticlePlaying())
            {
                _lighter.PlayParticleFire();
            }
        }
        else
        {
            _lighter.StopParticleFire();
        }
    }

    private void PlaySpoonParticle()
    {
        if (_drag.GetIsDragging() && _spoon.GetIsHeating())
        {
            if (!_spoon.CheckParticlePlaying())
            {
                _spoon.PlayParticleSmoke();
            }
        }
        else
        {
            _spoon.StopParticleSmoke();
        }
    }

}
