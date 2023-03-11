using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    

    public PlayerController playerController;

    public Transform playerTransform;

    public int digitosDistancia;
    public int digitosTiempo;
    public int digitosMinerales;
    public int digitosEnergia;

    public Text score;

    public int distance;
    private float _startTime;
    private float _totalTime;
    public int time;
    public int minerals;
    public int rocket;

    public bool isGameOver;

    public GameObject goArrow;
    public AudioSource arrowSound;

    //private bool _playArrowSound = false;
    private bool _activateInitialArrow = true;

    void Start()
    {
        _startTime = Time.time;
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {

        if ((playerTransform.position.x / 8f > distance) & !isGameOver)
        {
            distance = (int)(playerTransform.position.x / 8f);

        }
    }

    void FixedUpdate()
    {
        _totalTime = Time.time;
    }

    void LateUpdate()
    {

        string distancia = distance.ToString();
        while (distancia.Length < digitosDistancia)
        {
            distancia = "0" + distancia;
        }
        if (distancia.Length > digitosDistancia)
        {
            distancia = "";
            while (distancia.Length < digitosDistancia)
            {
                distancia = "9" + distancia;
            }
        }
        if (!isGameOver)
        {
            time = (int)(_totalTime - _startTime);
        }
        string tiempo = time.ToString();
        while (tiempo.Length < digitosTiempo)
        {
            tiempo = "0" + tiempo;
        }
        if (tiempo.Length > digitosTiempo)
        {
            tiempo = "";
            while (tiempo.Length < digitosTiempo)
            {
                tiempo = "9" + tiempo;
            }
        }

        string minerales = minerals.ToString();
        while (minerales.Length < digitosMinerales)
        {
            minerales = "0" + minerales;
        }
        if (minerales.Length > digitosMinerales)
        {
            minerales = "";
            while (minerales.Length < digitosMinerales)
            {
                minerales = "9" + minerales;
            }
        }

        if(!isGameOver)
        {
            rocket = Mathf.CeilToInt(playerController.powerRocket);
        }
        string energia = rocket.ToString();
        while (energia.Length < digitosEnergia)
        {
            energia = "0" + energia;
        }

        string textScore = "rock:" + energia + "  mine:" + minerales + "  time:" + tiempo + "  dist:" + distancia;
        score.text = textScore;

        //if (
        //    ((_totalTime - _startTime > 6f) & (_totalTime - _startTime < 6.5f)) ||
        //    ((_totalTime - _startTime > 7f) & (_totalTime - _startTime < 7.5f)) ||
        //    ((_totalTime - _startTime > 8f) & (_totalTime - _startTime < 8.5f))
        //    )
        //{
        //    goArrow.SetActive(true);
        //    if (!_playArrowSound)
        //    {
        //        arrowSound.Play();
        //        _playArrowSound = true;
        //    }
        //}
        //else
        //{
        //    goArrow.SetActive(false);
        //    _playArrowSound = false;
        //}
        
        if ((_totalTime - _startTime > 6f) & _activateInitialArrow)
        {
            StartCoroutine("ActivateArrow");
            _activateInitialArrow = false;
        }
    }

    public void AddMineralScore()
    {
        minerals++;
    }

    IEnumerator ActivateArrow()
    {
        goArrow.SetActive(true);
        arrowSound.Play();
        yield return new WaitForSeconds(0.5f);
        goArrow.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        goArrow.SetActive(true);
        arrowSound.Play();
        yield return new WaitForSeconds(0.5f);
        goArrow.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        goArrow.SetActive(true);
        arrowSound.Play();
        yield return new WaitForSeconds(0.5f);
        goArrow.SetActive(false);
    }
}
