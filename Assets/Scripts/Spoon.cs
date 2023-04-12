using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spoon : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSmoke = null;
    private bool _isHeating = false;
    private float _speedHeating = 0.001f;
    private UILighter _ui;
    private Dragging _drag;

    public bool GetIsHeating()
    {
        return _isHeating;
    }

    public void PlayParticleSmoke()
    {
        _particleSmoke.Play();
        
    }

    public void StopParticleSmoke()
    {
        _particleSmoke.Stop();
    }

    public bool CheckParticlePlaying()
    {
        return _particleSmoke.isPlaying;
    }


    private void Start()
    {
        _drag = FindObjectOfType<Dragging>();
        _ui = FindObjectOfType<UILighter>();
    }


    private void OnTriggerStay(Collider other)
    {
        if (_drag.GetIsDragging())
        {
            _ui.AddPointsSlider(_speedHeating);
            _isHeating = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _isHeating= false;
    }
    
}
