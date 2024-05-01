using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartMenu : MonoBehaviour
{
    public Canvas uiCanvas;

    public Button playButton;
    public Button optionsButton;
    public Button scoresButton;
    public Button quitButton;

    public AudioSource buttonPressed;
    //public AudioSource menuMusic;

    public float secondsWait;

    public GameObject letterR;
    public GameObject letterV;
    public GameObject letterA;

    private GameObject _selectedObject;

    

    void Start()
    {
        //int height = Screen.height;
        int _scaleFactor = GetScaleFactor();
        uiCanvas.scaleFactor = _scaleFactor;

        playButton.Select();

        _selectedObject = new GameObject();
    }

    void Awake()
    {
        //menuMusic.time = PlayerPrefs.GetFloat("MenuMusicTime", 0f);
        AudioListener.pause = false;
        
    }

    void Update()
    {
        //int height = Screen.height;
        int _scaleFactor = GetScaleFactor();
        uiCanvas.scaleFactor = _scaleFactor;

        // RESETEAR HIGH SCORES - SOLO MODO DESARROLLO
        if (Input.GetKey("r") & Input.GetKey("v") & Input.GetKey("a"))
        {
            PlayerPrefs.DeleteKey("HighScoresTable");
            PlayerPrefs.DeleteKey("MusicVolume");
            PlayerPrefs.DeleteKey("FxVolume");
            PlayerPrefs.DeleteKey("ScreenEffect");

            this.letterR.SetActive(true);
            this.letterV.SetActive(true);
            this.letterA.SetActive(true);
        }
        else
        {
            this.letterR.SetActive(false);
            this.letterV.SetActive(false);
            this.letterA.SetActive(false);
        }

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(_selectedObject);
        }
        else
        {
            _selectedObject = EventSystem.current.currentSelectedGameObject;
        }


    }

    void Play()
    {
        Destroy(GameObject.Find("MenuMusic"));
        buttonPressed.Play();
        //menuMusic.Stop();
        playButton.GetComponent<Animator>().SetBool("Pressed", true);
        StartCoroutine("LoadGame");

        
    }
    IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(secondsWait);
        //PlayerPrefs.SetFloat("MenuMusicTime", 0f);
        SceneManager.LoadScene("Game");
    }

    public void Scores()
    {
        buttonPressed.Play();
        scoresButton.GetComponent<Animator>().SetBool("Pressed", true);
        StartCoroutine("LoadScores");
        
    }
    IEnumerator LoadScores()
    {
        yield return new WaitForSeconds(secondsWait);
        //PlayerPrefs.SetFloat("MenuMusicTime", menuMusic.time);
        SceneManager.LoadScene("HighScores");
    }

    public void Options()
    {
        buttonPressed.Play();
        optionsButton.GetComponent<Animator>().SetBool("Pressed", true);
        StartCoroutine("LoadOptions");

    }
    IEnumerator LoadOptions()
    {
        yield return new WaitForSeconds(secondsWait);
        //PlayerPrefs.SetFloat("MenuMusicTime", menuMusic.time);
        SceneManager.LoadScene("Options");
    }

    public void Quit()
    {
        Destroy(GameObject.Find("MenuMusic"));
        //menuMusic.Stop();
        buttonPressed.Play();
        quitButton.GetComponent<Animator>().SetBool("Pressed", true);
        StartCoroutine("LoadQuit");
    }
    IEnumerator LoadQuit()
    {
        yield return new WaitForSeconds(secondsWait);
        //PlayerPrefs.SetFloat("MenuMusicTime", 0f);
        Application.Quit();
    }

    private int GetScaleFactor()
    {
        int ret;
        if (((decimal)Screen.height / (decimal)Screen.width) > ((decimal)180 / (decimal)320))
        {
            ret = Screen.width / 320;
        }
        else
        {
            ret = Screen.height / 180;
        }
        return ret;
    }
}
