using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenEffectController : MonoBehaviour
{
    public CRT screenCRT;
    public LCD screenLCD;

    private int _screenEffect;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        _screenEffect = PlayerPrefs.GetInt("ScreenEffect", -1);
        if (_screenEffect == -1)
        {
            PlayerPrefs.SetInt("ScreenEffect", 0);
            PlayerPrefs.Save();
            _screenEffect = PlayerPrefs.GetInt("ScreenEffect");
        }

        this.UpdateScreenEffect(_screenEffect);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScreenEffect(int screenEffect)
    {
        
        switch (screenEffect)
        {
            case 0:
                screenCRT.enabled = false;
                screenLCD.enabled = false;
                break;
            case 1:
                screenCRT.enabled = true;
                screenLCD.enabled = false;
                break;
            case 2:
                screenCRT.enabled = false;
                screenLCD.enabled = true;
                break;
        }
    }
}
