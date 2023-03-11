using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxController : MonoBehaviour
{

    [Range(0f, 1f)]
    public float maxFxVolume;

    private int _fxVolume;

    void Awake()
    {
        _fxVolume = PlayerPrefs.GetInt("FxVolume", -1);
        if (_fxVolume == -1)
        {
            PlayerPrefs.SetInt("FxVolume", 8);
            PlayerPrefs.Save();
            _fxVolume = PlayerPrefs.GetInt("FxVolume");
        }

        this.UpdateFxVolume(_fxVolume);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateFxVolume(int fxVolume)
    {
        gameObject.GetComponent<AudioSource>().volume = maxFxVolume * (float)fxVolume * (float)fxVolume / 100f;
    }
}
