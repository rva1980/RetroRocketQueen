using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    public static bool isPaused;

    public GameObject pauseMenuUi;

    public Button resumeButton;
    public Button retryButton;
    public Button exitButton;

    public AudioSource selectSound;

    public bool isGameOver;

    private GameObject _selectedObject;


    void Start()
    {
        _selectedObject = new GameObject();
    }

    void Awake()
    {
        AudioListener.pause = false;

        isPaused = false;

        selectSound.ignoreListenerPause = true;

        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") & !isGameOver)
        {
            if (isPaused)
            {
                Resume();

            }
            else
            {
                Pause();
            }
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

    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;
        isPaused = false;

    }

    void Pause() 
    {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        exitButton.Select();
        resumeButton.Select();
        AudioListener.pause = true;

    }


    public void Retry()
    {
        Time.timeScale = 1f;
        isPaused = false;
        //AudioListener.pause = false;
        SceneManager.LoadScene("Game");
    }


    public void Exit()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("StartMenu");
        //AudioListener.pause = false;
    }

    public void GameOver()
    {
        //Time.timeScale = 0f;
        //isPaused = true;
        isGameOver = true;
    }
}
