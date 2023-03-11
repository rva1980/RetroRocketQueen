using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetController : MonoBehaviour
{
    public Vector2 startMovement;

  
    public GameObject helmetSparkle;
    public GameObject helmetExplosion;

    public Transform headTransform;

    private Rigidbody2D _rigidbody;
    private Transform _transform;

    private GameObject _player;


    void Awake()
    {

        _rigidbody = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();

        _player = GameObject.Find("Player");

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

            GameObject instantiatedHelmetSparkle = Instantiate(helmetSparkle, _transform.position, Quaternion.identity);
            HelmetSparkleController instantiatedHelmetSparkleController = instantiatedHelmetSparkle.GetComponent<HelmetSparkleController>();
            instantiatedHelmetSparkleController.headTransform = headTransform;
            Destroy(instantiatedHelmetSparkle, 2f);
            PlayerController _playerController = _player.GetComponent<PlayerController>();
            _playerController.Heal();
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Geyser")
        {
            Destroy(gameObject);

            GameObject instanciatedHelmetExplosion = Instantiate(helmetExplosion, _transform.position, Quaternion.identity);
            Destroy(instanciatedHelmetExplosion, 2f);
        }
    }
}
