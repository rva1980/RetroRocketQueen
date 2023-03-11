using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicController : MonoBehaviour
{
    private AudioSource _gameMusic;
    private bool _musicOn;

    public AudioClip gameMusicLoop;

    void Awake()
    {
        _gameMusic = GetComponent<AudioSource>();
        _musicOn = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("PlayGameMusic");
        
    }

    IEnumerator PlayGameMusic()
    {
        yield return new WaitForSeconds(1.8f);
        _gameMusic.Play();
        _musicOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gameMusic.isPlaying && _musicOn && !AudioListener.pause)
        {
            _gameMusic.clip = gameMusicLoop;
            _gameMusic.loop = true;
            _gameMusic.Play();
        }
    }

    public void StopMusic()
    {
        _musicOn = false;
        _gameMusic.Stop();
    }

}
