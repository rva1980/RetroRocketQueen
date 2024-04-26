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
        //int height = Screen.height;
        int _scaleFactor = GetScaleFactor();
        scoreCanvas.scaleFactor = _scaleFactor;
        pauseCanvas.scaleFactor = _scaleFactor;
        gameoverCanvas.scaleFactor = _scaleFactor;
        
    }

    void Update()
    {
        //int height = Screen.height;
        int _scaleFactor = GetScaleFactor();
        scoreCanvas.scaleFactor = _scaleFactor;
        pauseCanvas.scaleFactor = _scaleFactor;
        gameoverCanvas.scaleFactor = _scaleFactor;

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
