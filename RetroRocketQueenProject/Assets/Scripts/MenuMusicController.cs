using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicController : MonoBehaviour
{
    private MenuMusicController _instance;

    void Awake()
    {
        if (FindObjectsOfType<MenuMusicController>().Length > 1)
        {
            Destroy(gameObject);
        }

        //if (_instance != null && _instance != this)
        //{
        //    Destroy(gameObject);
        //    return;
        //}
        //else
        //{
        //    _instance = this;
        //}

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
