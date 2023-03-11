using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralController : MonoBehaviour
{
    public Vector2 startMovement;

    public LayerMask playerLayer;

    public ScoreController scoreController;

    public GameObject mineralSparkle;
    public GameObject mineralExplosion;

    private Rigidbody2D _rigidbody;
    private Transform _transform;
    

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();

    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody.AddForce(startMovement, ForceMode2D.Impulse);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_transform.position.y < -100)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);

            GameObject instanciatedMineralSparkle = Instantiate(mineralSparkle, _transform.position, Quaternion.identity);
            Destroy(instanciatedMineralSparkle, 2f);
           
            scoreController.AddMineralScore();
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Geyser")
        {
            Destroy(gameObject);

            GameObject instanciatedMineralExplosion = Instantiate(mineralExplosion, _transform.position, Quaternion.identity);
            Destroy(instanciatedMineralExplosion, 2f);
        }
    }

}
