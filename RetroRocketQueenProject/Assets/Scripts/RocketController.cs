using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    private ParticleSystem _lightParticleSystem;
    //private ParticleSystem _darkParticleSystem;

    private AudioSource _audio;

    void Awake()
    {
        _lightParticleSystem = GameObject.Find("LightParticleSystem").GetComponent<ParticleSystem>();
        //_darkParticleSystem = GameObject.Find("DarkParticleSystem").GetComponent<ParticleSystem>();

        _audio = GetComponent<AudioSource>();
    }

    public void ParticleStart()
    {
        if (_lightParticleSystem.isStopped)
        {
            _lightParticleSystem.Play();
            _audio.Play();
        }

        //if (_darkParticleSystem.isStopped)
        //{
        //    _darkParticleSystem.Play();
        //}
    }

    public void ParticleStop()
    {
        if (_lightParticleSystem.isPlaying)
        {
            _lightParticleSystem.Stop();
            _audio.Stop();
        }

        //if (_darkParticleSystem.isPlaying)
        //{
        //    _darkParticleSystem.Stop();
        //}
    }
}
