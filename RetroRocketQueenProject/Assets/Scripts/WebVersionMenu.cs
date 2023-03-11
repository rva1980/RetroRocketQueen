using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class WebVersionMenu : MonoBehaviour
{
    public Canvas uiCanvas;

    public Button understoodButton;

    public float secondsWait;

    public AudioSource buttonPressed;

    private GameObject _selectedObject;


    void Start()
    {
        int height = Screen.height;
        int _scaleFactor = height / 180;
        uiCanvas.scaleFactor = _scaleFactor;

        understoodButton.Select();

        _selectedObject = new GameObject();
    }

    void Update()
    {
        int height = Screen.height;
        int _scaleFactor = height / 180;
        uiCanvas.scaleFactor = _scaleFactor;

        if (Input.GetButtonDown("Cancel"))
        {
            Understood();
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

    void Awake()
    {
        

    }

    

    public void Understood()
    {
        buttonPressed.Play();
        understoodButton.GetComponent<Animator>().SetBool("Pressed", true);
        StartCoroutine("LoadStartMenu");
    }
    IEnumerator LoadStartMenu()
    {
        yield return new WaitForSeconds(secondsWait);
        SceneManager.LoadScene("StartMenu");
    }

    

}
