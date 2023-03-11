using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MeteorController : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;

    public Transform pointTopRight;
    public Transform pointBottomRight;
    public Transform pointBottomLeft;
    public Transform pointTopLeft;

    public GameObject meteorExplosion;
    public GameObject tileExplosion;
    public GameObject helmetExplosion;
    public GameObject mineralExplosion;
    public GameObject hurtExplosion;

    public Tilemap grassTilemap;
    public Tilemap mineralTilemap;

    public GameObject mineralPrefab;

    public ScoreController scoreController;
    public PlayerController playerController;
    public CameraController cameraController;

    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private ParticleSystem _particleSystem;
    private AudioSource _audio;

    private Vector2 _startMovement;

    void Awake()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _startMovement = new Vector2(Random.Range(-1f, 1f), -1).normalized * Random.Range(minSpeed, maxSpeed);
        _rigidbody.velocity = _startMovement;

        _audio = GetComponent<AudioSource>();
        _audio.pitch = 0 + _rigidbody.velocity.magnitude / 100;

        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _particleSystem.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        GameObject instanciatedMeteorExplosion = Instantiate(meteorExplosion, gameObject.transform.position, Quaternion.identity);
        instanciatedMeteorExplosion.GetComponent<AudioSource>().pitch = Random.Range(0.5f, 2f);
        instanciatedMeteorExplosion.GetComponent<AudioSource>().Play();
        Destroy(instanciatedMeteorExplosion, 2f);

        if (collision.gameObject.tag == "Ground")
        {
            Tilemap groundTilemap = collision.gameObject.GetComponent<Tilemap>();

            if (groundTilemap.HasTile(groundTilemap.WorldToCell(pointTopRight.position)))
            {
                groundTilemap.SetTile(groundTilemap.WorldToCell(pointTopRight.position), null);
                GameObject instanciatedTileExplosion = Instantiate(tileExplosion, pointTopRight.position, Quaternion.identity);
                Destroy(instanciatedTileExplosion, 2f);
                CheckGrassTilemap(pointTopRight.position);
                CheckMineralTilemap(pointTopRight.position);
            }
            if (groundTilemap.HasTile(groundTilemap.WorldToCell(pointBottomRight.position)))
            {
                groundTilemap.SetTile(groundTilemap.WorldToCell(pointBottomRight.position), null);
                GameObject instanciatedTileExplosion = Instantiate(tileExplosion, pointBottomRight.position, Quaternion.identity);
                Destroy(instanciatedTileExplosion, 2f);
                CheckGrassTilemap(pointBottomRight.position);
                CheckMineralTilemap(pointBottomRight.position);
            }
            if (groundTilemap.HasTile(groundTilemap.WorldToCell(pointBottomLeft.position)))
            {
                groundTilemap.SetTile(groundTilemap.WorldToCell(pointBottomLeft.position), null);
                GameObject instanciatedTileExplosion = Instantiate(tileExplosion, pointBottomLeft.position, Quaternion.identity);
                Destroy(instanciatedTileExplosion, 2f);
                CheckGrassTilemap(pointBottomLeft.position);
                CheckMineralTilemap(pointBottomLeft.position);
            }
            if (groundTilemap.HasTile(groundTilemap.WorldToCell(pointTopLeft.position)))
            {
                groundTilemap.SetTile(groundTilemap.WorldToCell(pointTopLeft.position), null);
                GameObject instanciatedTileExplosion = Instantiate(tileExplosion, pointTopLeft.position, Quaternion.identity);
                Destroy(instanciatedTileExplosion, 2f);
                CheckGrassTilemap(pointTopLeft.position);
                CheckMineralTilemap(pointTopLeft.position);
            }

            if (!playerController.IsGameOver())
            {
                float distance = Absolute(playerController.gameObject.GetComponent<Transform>().position.x - _transform.position.x);
                if (distance < 40f)
                {
                    cameraController.Shake(2);
                }
                else if (distance < 120f)
                {
                    cameraController.Shake(1);
                }
            }

        }
        else if (collision.gameObject.tag == "Rock")
        {
            if (!playerController.IsGameOver())
            {
                float distance = Absolute(playerController.gameObject.GetComponent<Transform>().position.x - _transform.position.x);
                if (distance < 40f)
                {
                    cameraController.Shake(2);
                }
                else if (distance < 120f)
                {
                    cameraController.Shake(1);
                }
            }
        }
        else if (collision.gameObject.tag == "Player")
        {
            playerController.Hurt(_startMovement);
            GameObject instanciatedHurtExplosion = Instantiate(hurtExplosion, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(instanciatedHurtExplosion, 2f);
        }
        else if (collision.gameObject.tag == "Gemstone")
        {
            Destroy(collision.gameObject);

            GameObject instanciatedMineralExplosion = Instantiate(mineralExplosion, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(instanciatedMineralExplosion, 2f);
        }
        else if (collision.gameObject.tag == "Helmet")
        {
            Destroy(collision.gameObject);

            GameObject instanciatedHelmetExplosion = Instantiate(helmetExplosion, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(instanciatedHelmetExplosion, 2f);
        }
    }

    private void CheckGrassTilemap(Vector3 hitPosition)
    {
        Vector3Int checkPosition = grassTilemap.WorldToCell(new Vector3(hitPosition.x, hitPosition.y + 8f, hitPosition.z));
        if (grassTilemap.HasTile(checkPosition))
        {
            grassTilemap.SetTile(checkPosition, null);
        }
    }

    private void CheckMineralTilemap(Vector3 hitPosition)
    {
        Vector3Int checkPosition = mineralTilemap.WorldToCell(hitPosition);
        if (mineralTilemap.HasTile(checkPosition))
        {
            mineralTilemap.SetTile(checkPosition, null);
            GameObject instantiatedMineral = Instantiate(mineralPrefab, hitPosition, Quaternion.identity) as GameObject;
            MineralController instantiatedMineralController = instantiatedMineral.GetComponent<MineralController>();
            instantiatedMineralController.startMovement = new Vector2(_startMovement.x, _startMovement.y * -1);
            instantiatedMineralController.scoreController = scoreController;
        }
    }

    private float Absolute(float val)
    {
        if (val < 0)
        {
            val = - val;
        }
        return val;
    }
}
