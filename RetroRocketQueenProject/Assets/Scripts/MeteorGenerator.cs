using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MeteorGenerator : MonoBehaviour
{
    public Transform playerTransform;

    public GameObject meteorPrefab;

    public float desde;
    public float hasta;

    public float livingTime;

    public Tilemap grassTilemap;
    public Tilemap mineralTilemap;

    public ScoreController scoreController;
    public PlayerController playerController;
    public CameraController cameraController;

    private float _timer;
    private float _time;
    private float _resetTime;



    void Update()
    {
        _timer += Time.deltaTime;
        _time += Time.deltaTime;
        _resetTime = 10f / (1f + (_time / 10f));
    }

    void LateUpdate()
    {
        if (_timer >= _resetTime)
        {
            _timer = 0;

            if (_time > 10f)
            {
                InstanciateMeteor();
            }
        }
    }

    void InstanciateMeteor()
    {
        float modificadorX = Random.Range(desde * 8, hasta * 8);

        Vector3 point = new Vector3(playerTransform.position.x + modificadorX, 10 * 8, 0);

        GameObject instantiatedMeteor = Instantiate(meteorPrefab, point, Quaternion.identity) as GameObject;
        MeteorController instanciatedMeteorController = instantiatedMeteor.GetComponent<MeteorController>();
        instanciatedMeteorController.grassTilemap = grassTilemap;
        instanciatedMeteorController.mineralTilemap = mineralTilemap;
        instanciatedMeteorController.scoreController = scoreController;
        instanciatedMeteorController.playerController = playerController;
        instanciatedMeteorController.cameraController = cameraController;

        if (livingTime > 0f)
        {
            Destroy(instantiatedMeteor, livingTime);
        }


    }


   
}
