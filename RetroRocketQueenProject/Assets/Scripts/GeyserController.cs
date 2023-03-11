using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GeyserController : MonoBehaviour
{
    public Transform playerTransform;

    public Tilemap groundTilemap;
    public Tilemap rockTilemap;
    public Tilemap grassTilemap;
    public Tilemap mineralTilemap;

    public Transform pointLevel1;
    public Transform pointLevel2;
    public Transform pointLevel3;
    public Transform pointLevel4;
    public Transform pointLevel5;
    public Transform pointLevel6;
    public Transform pointLevel7;
    public Transform pointLevel8;
    public Transform pointLevel9;
    public Transform pointLevel10;
    public Transform pointLevel11;


    public GameObject tileExplosion;

    private AudioSource _waterSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        _waterSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (playerTransform.position.x - gameObject.transform.position.x < 16f)
        {
            if (rockTilemap.HasTile(rockTilemap.WorldToCell(pointLevel11.position)))
            {
                rockTilemap.SetTile(rockTilemap.WorldToCell(pointLevel11.position), null);
                GameObject instantiatedTileExplosion = Instantiate(tileExplosion, pointLevel11.position, Quaternion.identity);
                Destroy(instantiatedTileExplosion, 2f);
            }
            if (groundTilemap.HasTile(groundTilemap.WorldToCell(pointLevel11.position)))
            {
                groundTilemap.SetTile(groundTilemap.WorldToCell(pointLevel11.position), null);
                mineralTilemap.SetTile(mineralTilemap.WorldToCell(pointLevel11.position), null);
                GameObject instantiatedTileExplosion = Instantiate(tileExplosion, pointLevel11.position, Quaternion.identity);
                Destroy(instantiatedTileExplosion, 2f);
            }
            if (grassTilemap.HasTile(grassTilemap.WorldToCell(pointLevel11.position)))
            {
                grassTilemap.SetTile(grassTilemap.WorldToCell(pointLevel11.position), null);
            }
        }
        else if (playerTransform.position.x - gameObject.transform.position.x < 24f)
        {
            if (rockTilemap.HasTile(rockTilemap.WorldToCell(pointLevel10.position)))
            {
                rockTilemap.SetTile(rockTilemap.WorldToCell(pointLevel10.position), null);
                GameObject instantiatedTileExplosion = Instantiate(tileExplosion, pointLevel10.position, Quaternion.identity);
                Destroy(instantiatedTileExplosion, 2f);
            }
            if (groundTilemap.HasTile(groundTilemap.WorldToCell(pointLevel10.position)))
            {
                groundTilemap.SetTile(groundTilemap.WorldToCell(pointLevel10.position), null);
                mineralTilemap.SetTile(mineralTilemap.WorldToCell(pointLevel10.position), null);
                GameObject instantiatedTileExplosion = Instantiate(tileExplosion, pointLevel10.position, Quaternion.identity);
                Destroy(instantiatedTileExplosion, 2f);
            }
            if (grassTilemap.HasTile(grassTilemap.WorldToCell(pointLevel10.position)))
            {
                grassTilemap.SetTile(grassTilemap.WorldToCell(pointLevel10.position), null);
            }
        }
        else if (playerTransform.position.x - gameObject.transform.position.x < 32f)
        {
            if (rockTilemap.HasTile(rockTilemap.WorldToCell(pointLevel9.position)))
            {
                rockTilemap.SetTile(rockTilemap.WorldToCell(pointLevel9.position), null);
                GameObject instantiatedTileExplosion = Instantiate(tileExplosion, pointLevel9.position, Quaternion.identity);
                Destroy(instantiatedTileExplosion, 2f);
            }
            if (groundTilemap.HasTile(groundTilemap.WorldToCell(pointLevel9.position)))
            {
                groundTilemap.SetTile(groundTilemap.WorldToCell(pointLevel9.position), null);
                mineralTilemap.SetTile(mineralTilemap.WorldToCell(pointLevel9.position), null);
                GameObject instantiatedTileExplosion = Instantiate(tileExplosion, pointLevel9.position, Quaternion.identity);
                Destroy(instantiatedTileExplosion, 2f);
            }
            if (grassTilemap.HasTile(grassTilemap.WorldToCell(pointLevel9.position)))
            {
                grassTilemap.SetTile(grassTilemap.WorldToCell(pointLevel9.position), null);
            }
        }
        else if (playerTransform.position.x - gameObject.transform.position.x < 40f)
        {
            if (rockTilemap.HasTile(rockTilemap.WorldToCell(pointLevel8.position)))
            {
                rockTilemap.SetTile(rockTilemap.WorldToCell(pointLevel8.position), null);
                GameObject instantiatedTileExplosion = Instantiate(tileExplosion, pointLevel8.position, Quaternion.identity);
                Destroy(instantiatedTileExplosion, 2f);
            }
            if (groundTilemap.HasTile(groundTilemap.WorldToCell(pointLevel8.position)))
            {
                groundTilemap.SetTile(groundTilemap.WorldToCell(pointLevel8.position), null);
                mineralTilemap.SetTile(mineralTilemap.WorldToCell(pointLevel8.position), null);
                GameObject instantiatedTileExplosion = Instantiate(tileExplosion, pointLevel8.position, Quaternion.identity);
                Destroy(instantiatedTileExplosion, 2f);
            }
            if (grassTilemap.HasTile(grassTilemap.WorldToCell(pointLevel8.position)))
            {
                grassTilemap.SetTile(grassTilemap.WorldToCell(pointLevel8.position), null);
            }
        }
        else if (playerTransform.position.x - gameObject.transform.position.x < 48f)
        {
            if (rockTilemap.HasTile(rockTilemap.WorldToCell(pointLevel7.position)))
            {
                rockTilemap.SetTile(rockTilemap.WorldToCell(pointLevel7.position), null);
                GameObject instantiatedTileExplosion = Instantiate(tileExplosion, pointLevel7.position, Quaternion.identity);
                Destroy(instantiatedTileExplosion, 2f);
            }
            if (groundTilemap.HasTile(groundTilemap.WorldToCell(pointLevel7.position)))
            {
                groundTilemap.SetTile(groundTilemap.WorldToCell(pointLevel7.position), null);
                mineralTilemap.SetTile(mineralTilemap.WorldToCell(pointLevel7.position), null);
                GameObject instantiatedTileExplosion = Instantiate(tileExplosion, pointLevel7.position, Quaternion.identity);
                Destroy(instantiatedTileExplosion, 2f);
            }
            if (grassTilemap.HasTile(grassTilemap.WorldToCell(pointLevel7.position)))
            {
                grassTilemap.SetTile(grassTilemap.WorldToCell(pointLevel7.position), null);
            }
        }
        else if (playerTransform.position.x - gameObject.transform.position.x < 56f)
        {
            if (rockTilemap.HasTile(rockTilemap.WorldToCell(pointLevel6.position)))
            {
                rockTilemap.SetTile(rockTilemap.WorldToCell(pointLevel6.position), null);
                GameObject instantiatedTileExplosion = Instantiate(tileExplosion, pointLevel6.position, Quaternion.identity);
                Destroy(instantiatedTileExplosion, 2f);
            }
            if (groundTilemap.HasTile(groundTilemap.WorldToCell(pointLevel6.position)))
            {
                groundTilemap.SetTile(groundTilemap.WorldToCell(pointLevel6.position), null);
                mineralTilemap.SetTile(mineralTilemap.WorldToCell(pointLevel6.position), null);
                GameObject instantiatedTileExplosion = Instantiate(tileExplosion, pointLevel6.position, Quaternion.identity);
                Destroy(instantiatedTileExplosion, 2f);
            }
            if (grassTilemap.HasTile(grassTilemap.WorldToCell(pointLevel6.position)))
            {
                grassTilemap.SetTile(grassTilemap.WorldToCell(pointLevel6.position), null);
            }
        }
        else if (playerTransform.position.x - gameObject.transform.position.x < 64f)
        {
            if (rockTilemap.HasTile(rockTilemap.WorldToCell(pointLevel5.position)))
            {
                rockTilemap.SetTile(rockTilemap.WorldToCell(pointLevel5.position), null);
                GameObject instantiatedTileExplosion = Instantiate(tileExplosion, pointLevel5.position, Quaternion.identity);
                Destroy(instantiatedTileExplosion, 2f);
            }
            if (groundTilemap.HasTile(groundTilemap.WorldToCell(pointLevel5.position)))
            {
                groundTilemap.SetTile(groundTilemap.WorldToCell(pointLevel5.position), null);
                mineralTilemap.SetTile(mineralTilemap.WorldToCell(pointLevel5.position), null);
                GameObject instantiatedTileExplosion = Instantiate(tileExplosion, pointLevel5.position, Quaternion.identity);
                Destroy(instantiatedTileExplosion, 2f);
            }
            if (grassTilemap.HasTile(grassTilemap.WorldToCell(pointLevel5.position)))
            {
                grassTilemap.SetTile(grassTilemap.WorldToCell(pointLevel5.position), null);
            }
        }
        else if (playerTransform.position.x - gameObject.transform.position.x < 72f)
        {
            if (rockTilemap.HasTile(rockTilemap.WorldToCell(pointLevel4.position)))
            {
                rockTilemap.SetTile(rockTilemap.WorldToCell(pointLevel4.position), null);
                GameObject instantiatedTileExplosion = Instantiate(tileExplosion, pointLevel4.position, Quaternion.identity);
                Destroy(instantiatedTileExplosion, 2f);
            }
            if (groundTilemap.HasTile(groundTilemap.WorldToCell(pointLevel4.position)))
            {
                groundTilemap.SetTile(groundTilemap.WorldToCell(pointLevel4.position), null);
                mineralTilemap.SetTile(mineralTilemap.WorldToCell(pointLevel4.position), null);
                GameObject instantiatedTileExplosion = Instantiate(tileExplosion, pointLevel4.position, Quaternion.identity);
                Destroy(instantiatedTileExplosion, 2f);
            }
            if (grassTilemap.HasTile(grassTilemap.WorldToCell(pointLevel4.position)))
            {
                grassTilemap.SetTile(grassTilemap.WorldToCell(pointLevel4.position), null);
            }
        }
        else if (playerTransform.position.x - gameObject.transform.position.x < 80f)
        {
            if (rockTilemap.HasTile(rockTilemap.WorldToCell(pointLevel3.position)))
            {
                rockTilemap.SetTile(rockTilemap.WorldToCell(pointLevel3.position), null);
                GameObject instantiatedTileExplosion = Instantiate(tileExplosion, pointLevel3.position, Quaternion.identity);
                Destroy(instantiatedTileExplosion, 2f);
            }
            if (groundTilemap.HasTile(groundTilemap.WorldToCell(pointLevel3.position)))
            {
                groundTilemap.SetTile(groundTilemap.WorldToCell(pointLevel3.position), null);
                mineralTilemap.SetTile(mineralTilemap.WorldToCell(pointLevel3.position), null);
                GameObject instantiatedTileExplosion = Instantiate(tileExplosion, pointLevel3.position, Quaternion.identity);
                Destroy(instantiatedTileExplosion, 2f);
            }
            if (grassTilemap.HasTile(grassTilemap.WorldToCell(pointLevel3.position)))
            {
                grassTilemap.SetTile(grassTilemap.WorldToCell(pointLevel3.position), null);
            }
        }
        else if (playerTransform.position.x - gameObject.transform.position.x < 88f)
        {
            if (rockTilemap.HasTile(rockTilemap.WorldToCell(pointLevel2.position)))
            {
                rockTilemap.SetTile(rockTilemap.WorldToCell(pointLevel2.position), null);
                GameObject instantiatedTileExplosion = Instantiate(tileExplosion, pointLevel2.position, Quaternion.identity);
                Destroy(instantiatedTileExplosion, 2f);
            }
            if (groundTilemap.HasTile(groundTilemap.WorldToCell(pointLevel2.position)))
            {
                groundTilemap.SetTile(groundTilemap.WorldToCell(pointLevel2.position), null);
                mineralTilemap.SetTile(mineralTilemap.WorldToCell(pointLevel2.position), null);
                GameObject instantiatedTileExplosion = Instantiate(tileExplosion, pointLevel2.position, Quaternion.identity);
                Destroy(instantiatedTileExplosion, 2f);
            }
            if (grassTilemap.HasTile(grassTilemap.WorldToCell(pointLevel2.position)))
            {
                grassTilemap.SetTile(grassTilemap.WorldToCell(pointLevel2.position), null);
            }
        }
        else if (playerTransform.position.x - gameObject.transform.position.x < 96f)
        {
            if (rockTilemap.HasTile(rockTilemap.WorldToCell(pointLevel1.position)))
            {
                rockTilemap.SetTile(rockTilemap.WorldToCell(pointLevel1.position), null);
                GameObject instantiatedTileExplosion = Instantiate(tileExplosion, pointLevel1.position, Quaternion.identity);
                Destroy(instantiatedTileExplosion, 2f);
            }
            if (groundTilemap.HasTile(groundTilemap.WorldToCell(pointLevel1.position)))
            {
                groundTilemap.SetTile(groundTilemap.WorldToCell(pointLevel1.position), null);
                mineralTilemap.SetTile(mineralTilemap.WorldToCell(pointLevel1.position), null);
                GameObject instantiatedTileExplosion = Instantiate(tileExplosion, pointLevel1.position, Quaternion.identity);
                Destroy(instantiatedTileExplosion, 2f);
            }
            if (grassTilemap.HasTile(grassTilemap.WorldToCell(pointLevel1.position)))
            {
                grassTilemap.SetTile(grassTilemap.WorldToCell(pointLevel1.position), null);
            }
        }

        if (playerTransform.position.x - gameObject.transform.position.x > 480f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Meteor" || collision.gameObject.tag == "Gemstone" || collision.gameObject.tag == "Helmet")
        {
            _waterSound.Play();
        }
    }

}
