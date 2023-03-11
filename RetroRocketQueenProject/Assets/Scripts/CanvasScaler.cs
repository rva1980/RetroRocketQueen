using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScaler : MonoBehaviour
{
    public Canvas scoreCanvas;
    public Canvas pauseCanvas;
    public Canvas gameoverCanvas;

    // Update is called once per frame
    void Start()
    {
        int height = Screen.height;
        int _scaleFactor = height / 180;
        scoreCanvas.scaleFactor = _scaleFactor;
        pauseCanvas.scaleFactor = _scaleFactor;
        gameoverCanvas.scaleFactor = _scaleFactor;
        
    }

    void Update()
    {
        int height = Screen.height;
        int _scaleFactor = height / 180;
        scoreCanvas.scaleFactor = _scaleFactor;
        pauseCanvas.scaleFactor = _scaleFactor;
        gameoverCanvas.scaleFactor = _scaleFactor;

    }
}
