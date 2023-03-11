using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{

    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius;
    private bool _isGrounded;
    private bool _isGroundedOld;
    public ParticleSystem backDust1;
    public ParticleSystem frontDust1;
    public ParticleSystem backDust2;
    public ParticleSystem frontDust2;
    public ParticleSystem backDust3;
    public ParticleSystem frontDust3;
    public ParticleSystem backDust4;
    public ParticleSystem frontDust4;

    public CameraController cameraController;

    private Rigidbody2D _rigidbody;

    private AudioSource _audio;

    private AudioSource _landingSound;


    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _audio = GetComponent<AudioSource>();

        _landingSound = GameObject.Find("LandingSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Is grounded?
        _isGroundedOld = _isGrounded;
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (_isGrounded)
        {
            if (!_isGroundedOld)
            {
                backDust1.Play();
                frontDust1.Play();
                backDust2.Play();
                frontDust2.Play();
                backDust3.Play();
                frontDust3.Play();
                backDust4.Play();
                frontDust4.Play();

                _audio.Stop();

                _landingSound.Play();

                cameraController.Shake(1);
            }
        }
        //console.log(_isGrounded);

        
    }

    void FixedUpdate()
    {

        float shipSpeed = (-40 - this.transform.position.y) / 2;

        if (_rigidbody.velocity.y > shipSpeed)
        {
            shipSpeed = _rigidbody.velocity.y;
        }

        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, shipSpeed);

        _audio.pitch = 1 + this.transform.position.y / 500;
    }



}
