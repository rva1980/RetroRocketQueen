using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GeyserGenerator : MonoBehaviour
{
    public Transform playerTransform;

    public Tilemap groundTilemap;
    public Tilemap rockTilemap;
    public Tilemap grassTilemap;
    public Tilemap mineralTilemap;

    public GameObject geyserPrefab;

    public float backward;

    public GameObject goArrow;
    public AudioSource arrowSound;

    public PauseMenu pauseMenu;

    private float _distance;

    void Awake()
    {
        _distance = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.x > _distance)
        {
            _distance = playerTransform.position.x;
        }

        if (playerTransform.position.x < (_distance - backward))
        {
            int posX = ((Mathf.FloorToInt(playerTransform.position.x / 8f) - 12) * 8 - 4);

            GameObject instantiatedGeyser = Instantiate(geyserPrefab, new Vector3(posX, 0, 0), Quaternion.identity);
            GeyserController instanciatedGeyserController = instantiatedGeyser.GetComponent<GeyserController>();
            instanciatedGeyserController.playerTransform = playerTransform;
            instanciatedGeyserController.rockTilemap = rockTilemap;
            instanciatedGeyserController.groundTilemap = groundTilemap;
            instanciatedGeyserController.grassTilemap = grassTilemap;
            instanciatedGeyserController.mineralTilemap = mineralTilemap;

            StartCoroutine("ActivateArrow");

            _distance = playerTransform.position.x;
        }
    }

    IEnumerator ActivateArrow()
    {
        yield return new WaitForSeconds(4f);
        if (!pauseMenu.isGameOver)
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
}
