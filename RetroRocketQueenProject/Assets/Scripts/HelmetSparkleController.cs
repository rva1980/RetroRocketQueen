using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetSparkleController : MonoBehaviour
{
    public Transform headTransform;

    private Transform _transform;
    private Rigidbody2D _rigidbody;

    private Vector2 _movement;


    private void Awake()
    {
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
        _movement = new Vector2(headTransform.position.x - _transform.position.x, headTransform.position.y - _transform.position.y);
    }

    private void FixedUpdate()
    {
        //float horizontalVelocity = _movement.normalized.x * speed + _pushMovement * pushSpeed;
        _rigidbody.velocity = _movement * 40f;

    }
}
