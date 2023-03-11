using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    [Range(0f, 1f)]
    public float maxMusicVolume;
    private int _musicVolume;
    private int _fxVolume;

    // Start is called before the first frame update
    void Start()
    {
        _musicVolume = PlayerPrefs.GetInt("MusicVolume", -1);
        if (_musicVolume == -1)
        {
            PlayerPrefs.SetInt("MusicVolume", 8);
            PlayerPrefs.Save();
            _musicVolume = PlayerPrefs.GetInt("MusicVolume");
        }

        _fxVolume = PlayerPrefs.GetInt("FxVolume", -1);
        if (_fxVolume == -1)
        {
            PlayerPrefs.SetInt("FxVolume", 8);
            PlayerPrefs.Save();
            _fxVolume = PlayerPrefs.GetInt("FxVolume");
        }

        this.UpdateMusicVolume(_musicVolume);
        this.UpdateFxVolume(_fxVolume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMusicVolume(int musicVolume)
    {
        //AudioListener.volume = (float)musicVolume * (float)musicVolume / 100f;
        GameObject[] musicas = GameObject.FindGameObjectsWithTag("Music");

        foreach (GameObject musica in musicas)
        {
            
            musica.GetComponent<AudioSource>().volume = maxMusicVolume * (float)musicVolume * (float)musicVolume / 100f;
        }

    }
    public void UpdateFxVolume(int fxVolume)
    {
        //AudioListener.volume = (float)fxVolume * (float)fxVolume / 100f;
        GameObject[] fxs = GameObject.FindGameObjectsWithTag("FX");

        foreach (GameObject fx in fxs)
        {
            fx.GetComponent<AudioSource>().volume = (float)fxVolume * (float)fxVolume / 100f;
        }
    }
}
