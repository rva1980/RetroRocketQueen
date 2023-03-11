using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippedShipController : MonoBehaviour
{
    // Start is called before the first frame update
    

    private Transform _transform;
    private Rigidbody2D _rigidbody;

    private Vector2 _movement;
    private Vector2 _destiny;


    private void Awake()
    {
        _destiny = new Vector2(64f, -18);
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _movement = new Vector2(_destiny.x - _transform.position.x, _destiny.y - _transform.position.y);
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_movement.x / 4f, _movement.y / 2f);

    }
}
