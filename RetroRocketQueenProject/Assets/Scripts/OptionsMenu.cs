using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class OptionsMenu : MonoBehaviour
{
    public Canvas uiCanvas;

    public Button musicVolumeButton;
    public Button fxVolumeButton;
    public Button screenEffectButton;
    public Button backButton;

    public Text musicVolumeLine;
    public Text fxVolumeLine;
    public Text screenEffectLine;

    public Image musicVolumeSelector;
    public Image fxVolumeSelector;
    public Image screenEffectSelector;

    public float secondsWait;

    public AudioSource buttonPressed;
    public AudioSource selectSound;
    //public AudioSource menuMusic;

    private bool _horizontalDown;
    private float _horizontalInput;

    private GameObject _selectedObject;

    //OPTIONS

    private int _musicVolume;
    private int _fxVolume;
    private int _screenEffect;

    public ScreenEffectController screenEffectController;
    public VolumeController volumeController;

    void Awake()
    {
        //menuMusic.time = PlayerPrefs.GetFloat("MenuMusicTime", 0f);

        LoadMusicVolume();
        LoadFxVolume();
        LoadScreenEffect();

        UpdateMusicVolume();
        UpdateFxVolume();
        UpdateScreenEffect();
    }


    void Start()
    {
        musicVolumeButton.Select();

        _selectedObject = new GameObject();
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Back();
        }

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(_selectedObject);
        }
        else
        {
            _selectedObject = EventSystem.current.currentSelectedGameObject;
        }


        _horizontalInput = Input.GetAxisRaw("Horizontal");

        if (EventSystem.current.currentSelectedGameObject.name == "MusicVolumeButton")
        {
            musicVolumeSelector.enabled = true;
            fxVolumeSelector.enabled = false;
            screenEffectSelector.enabled = false;

            if (_horizontalInput > 0f && !_horizontalDown)
            {
                _horizontalDown = true;
                
                if (_musicVolume < 10)
                {
                    _musicVolume += 1;
                    selectSound.Play();
                    UpdateMusicVolume();
                    LoadMusicVolume();
                }
            }
            else if (_horizontalInput < 0f && !_horizontalDown)
            {
                _horizontalDown = true;

                if (_musicVolume >0)
                {
                    _musicVolume -= 1;
                    selectSound.Play();
                    UpdateMusicVolume();
                    LoadMusicVolume();
                }
            }
            else if (_horizontalInput == 0f)
            {
                _horizontalDown = false;
            }

        }
        else if (EventSystem.current.currentSelectedGameObject.name == "FxVolumeButton")
        {
            musicVolumeSelector.enabled = false;
            fxVolumeSelector.enabled = true;
            screenEffectSelector.enabled = false;

            if (_horizontalInput > 0f && !_horizontalDown)
            {
                _horizontalDown = true;

                if (_fxVolume < 10)
                {
                    _fxVolume += 1;
                    selectSound.Play();
                    UpdateFxVolume();
                    LoadFxVolume();
                }
            }
            else if (_horizontalInput < 0f && !_horizontalDown)
            {
                _horizontalDown = true;

                if (_fxVolume >0)
                {
                    _fxVolume -= 1;
                    selectSound.Play();
                    UpdateFxVolume();
                    LoadFxVolume();
                }
            }
            else if (_horizontalInput == 0f)
            {
                _horizontalDown = false;
            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "ScreenEffectButton")
        {
            musicVolumeSelector.enabled = false;
            fxVolumeSelector.enabled = false;
            screenEffectSelector.enabled = true;

            if (_horizontalInput > 0f && !_horizontalDown)
            {
                _horizontalDown = true;

                if (_screenEffect < 2)
                {
                    _screenEffect += 1;
                    selectSound.Play();
                    UpdateScreenEffect();
                    LoadScreenEffect();
                }
                else
                {
                    _screenEffect = 0;
                    selectSound.Play();
                    UpdateScreenEffect();
                    LoadScreenEffect();
                }
            }
            else if (_horizontalInput < 0f && !_horizontalDown)
            {
                _horizontalDown = true;

                if (_screenEffect > 0)
                {
                    _screenEffect -= 1;
                    selectSound.Play();
                    UpdateScreenEffect();
                    LoadScreenEffect();
                }
                else
                {
                    _screenEffect = 2;
                    selectSound.Play();
                    UpdateScreenEffect();
                    LoadScreenEffect();
                }
            }
            else if (_horizontalInput == 0f)
            {
                _horizontalDown = false;
            }
        }
        else
        {
            musicVolumeSelector.enabled = false;
            fxVolumeSelector.enabled = false;
            screenEffectSelector.enabled = false;
        }
    }

    
    public void Back()
    {
        buttonPressed.Play();
        backButton.GetComponent<Animator>().SetBool("Pressed", true);
        StartCoroutine("LoadBack");
    }
    IEnumerator LoadBack()
    {
        yield return new WaitForSeconds(secondsWait);
        //PlayerPrefs.SetFloat("MenuMusicTime", menuMusic.time);
        SceneManager.LoadScene("StartMenu");
    }

    private void UpdateMusicVolume()
    {
        PlayerPrefs.SetInt("MusicVolume", _musicVolume);
        PlayerPrefs.Save();

        musicVolumeLine.text = "";

        for (int i = 1; i <= 10; i++)
        {
            if (i <= _musicVolume)
            {
                musicVolumeLine.text += "@";
            }
            else
            {
                musicVolumeLine.text += "_";
            }
        }
    }

    private void UpdateFxVolume()
    { 
        PlayerPrefs.SetInt("FxVolume", _fxVolume);
        PlayerPrefs.Save();

        fxVolumeLine.text = "";

        for (int i = 1; i <= 10; i++)
        {
            if (i <= _fxVolume)
            {
                fxVolumeLine.text += "@";
            }
            else
            {
                fxVolumeLine.text += "_";
            }
        }
    }

    private void UpdateScreenEffect()
    {
        PlayerPrefs.SetInt("ScreenEffect", _screenEffect);
        PlayerPrefs.Save();


        screenEffectLine.text = "";

        switch (_screenEffect)
        {
            case 0: screenEffectLine.text += "off"; break;
            case 1: screenEffectLine.text += "crt"; break;
            case 2: screenEffectLine.text += "lcd"; break;
        }
    }

    public void LoadMusicVolume()
    {
        _musicVolume = PlayerPrefs.GetInt("MusicVolume", -1);
        if (_musicVolume == -1)
        {
            PlayerPrefs.SetInt("MusicVolume", 8);
            PlayerPrefs.Save();
            _musicVolume = PlayerPrefs.GetInt("MusicVolume");
        }


        volumeController.UpdateMusicVolume(_musicVolume);

    }

    public void LoadFxVolume()
    {
        _fxVolume = PlayerPrefs.GetInt("FxVolume", -1);
        if (_fxVolume == -1)
        {
            PlayerPrefs.SetInt("FxVolume", 8);
            PlayerPrefs.Save();
            _fxVolume = PlayerPrefs.GetInt("FxVolume");
        }

        volumeController.UpdateFxVolume(_fxVolume);

    }

    public void LoadScreenEffect()
    {
        _screenEffect = PlayerPrefs.GetInt("ScreenEffect", -1);
        if (_screenEffect == -1)
        {
            PlayerPrefs.SetInt("ScreenEffect", 0);
            PlayerPrefs.Save();
            _screenEffect = PlayerPrefs.GetInt("ScreenEffect");
        }

        screenEffectController.UpdateScreenEffect(_screenEffect);
    }
}
