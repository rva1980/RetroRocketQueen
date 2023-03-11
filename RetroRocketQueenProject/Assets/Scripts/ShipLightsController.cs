using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLightsController : MonoBehaviour
{
    private AudioSource _audio;

    // Start is called before the first frame update
    void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }


    private void bip()
    {
        _audio.Play();
    }
}
