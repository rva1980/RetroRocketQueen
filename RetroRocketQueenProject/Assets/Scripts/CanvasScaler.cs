using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScaler : MonoBehaviour
{
    [Range(0, 1)] public float waitTime = 0.2f;

    private Canvas _canvas;

    private float _startTime; 

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.scaleFactor = GetScaleFactor();
        _canvas.enabled = false;
        _startTime = Time.time;
    }

    // Start is called before the first frame update
    void Start()
    {
        _canvas.scaleFactor = GetScaleFactor();
    }

    // Update is called once per frame
    void Update()
    {
        _canvas.scaleFactor = GetScaleFactor();

        if (Time.time >= _startTime + waitTime)
        {
            _canvas.enabled = true;
        }       
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
