using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float pushSpeed;
    public float jumpForce;
    public float powerRocket;
    public float skyline;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public LayerMask rockLayer;
    public LayerMask shipLayer;

    public float groundCheckRadius;
    public float jumpCheckRadius;

    public GameObject rocket;
    public RocketController rocketController;

    //public GameObject helmet;
    //public GameObject head;
    private Transform _headTransform;

    public ParticleSystem backDust;
    public ParticleSystem frontDust;

    public PauseMenu pauseMenu;
    public GameOverMenu gameOverMenu;

    public bool lostHelmet;

    public SpriteRenderer flashBackgroundSprite;
    public Tilemap grass;
    public Tilemap ground;
    public Tilemap rock;
    public Tilemap mineral;
    private SpriteRenderer _frontArmSprite;
    private SpriteRenderer _frontLegSprite;
    private SpriteRenderer _helmetSprite; 
    private SpriteRenderer _headSprite;
    private SpriteRenderer _bodySprite;
    private SpriteRenderer _rocketSprite;
    private SpriteRenderer _backLegSprite;
    private SpriteRenderer _backArmSprite;
    private bool _isFlash;

    
    // References
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private float _time;

    // Movement
    private Vector2 _movement;
    private bool _facingRight = true;
    private float _pushMovement;
    private float _pushMovementStep;

    // Jumping
    private bool _canJump;
    private bool _isGrounded;
    private bool _canJumpOld;
    private bool _isOnShip;
    private float _jumpTime;
    

    // Rocket
    private bool _canRocket;
    private bool _isRocketing;

    // Damage
    private bool _isHurt;
    private bool _isGameOver;
    public GameObject helmetPrefab;

    // Audio
    private AudioSource _audio;
    private AudioSource _hitSound;
    private AudioSource _waterSound;
    private AudioSource _helmetUpSound;

    public GameMusicController gameMusicController;


    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _isHurt = false;
        _isGameOver = false;

        _audio = GetComponent<AudioSource>();

        _hitSound = GameObject.Find("HitSound").GetComponent<AudioSource>();
        _waterSound = GameObject.Find("WaterSound").GetComponent<AudioSource>();
        _helmetUpSound = GameObject.Find("HelmetUpSound").GetComponent<AudioSource>();

        _isFlash = false;
        
        _frontArmSprite = GameObject.Find("FrontArm").GetComponent<SpriteRenderer>();
        _frontLegSprite = GameObject.Find("FrontLeg").GetComponent<SpriteRenderer>();
        _helmetSprite = GameObject.Find("Helmet").GetComponent<SpriteRenderer>();
        _headSprite = GameObject.Find("Head").GetComponent<SpriteRenderer>();
        _bodySprite = GameObject.Find("Body").GetComponent<SpriteRenderer>();
        _rocketSprite = GameObject.Find("Rocket").GetComponent<SpriteRenderer>();
        _backLegSprite = GameObject.Find("BackLeg").GetComponent<SpriteRenderer>();
        _backArmSprite = GameObject.Find("BackArm").GetComponent<SpriteRenderer>();

        _headTransform = GameObject.Find("Head").GetComponent<Transform>();

        _pushMovement = 0f;
        _pushMovementStep = 0.002f;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;


        if (PauseMenu.isPaused == false && _isFlash == false)
        {
            // Movement
            float horizontalInput = 0f;
            if (_time > 6f & !_isGameOver)
            {
                horizontalInput = Input.GetAxisRaw("Horizontal");
            }

            _movement = new Vector2(horizontalInput, 0f);

            // Flip character
            if (horizontalInput < 0f && _facingRight == true)
            {
                Flip();
            }
            else if (horizontalInput > 0f && _facingRight == false)
            {
                Flip();
            }

            // Is grounded?
            _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer) || Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, rockLayer) || Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, shipLayer);

            // Can jump?
            _canJumpOld = _canJump;
            _canJump = Physics2D.OverlapCircle(groundCheck.position, jumpCheckRadius, groundLayer) || Physics2D.OverlapCircle(groundCheck.position, jumpCheckRadius, rockLayer) || Physics2D.OverlapCircle(groundCheck.position, jumpCheckRadius, shipLayer);
            if (_canJump)
            {
                _canRocket = false;
                if (!_canJumpOld)
                {
                    backDust.Play();
                    frontDust.Play();
                }
            }            

            // Is jumping?
            if (Input.GetButtonDown("Jump") && _canJump == true && _time > 6f && !_isGameOver)
            {
                _rigidbody.velocity = Vector2.zero;
                _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                backDust.Play();
                frontDust.Play();
                _audio.pitch = Random.Range(0.8f, 1.2f);
                _audio.Play();
                _jumpTime = Time.time;
            }

            // Can rocket in jump?
            if (Time.time > _jumpTime + 0.4f && _canRocket == false && _canJump == false)
            {
                _canRocket = true;
            }


            // Can rocket?
            if (Input.GetButtonDown("Jump") && _canJump == false)
            {
                _canRocket = true;
            }

            // Is Rocketing
            if (Input.GetButton("Jump") && _canRocket == true && powerRocket > 0 && !_isGameOver)
            {
                _isRocketing = true;
                rocketController.ParticleStart();

                powerRocket -= Time.deltaTime * 10;
                if (powerRocket <= 0)
                {
                    powerRocket = 0;
                    rocket.SetActive(false);
                }

            }
            else
            {
                _isRocketing = false;
                rocketController.ParticleStop();
            }
        }

        _helmetSprite.enabled = !_isHurt;
        _headSprite.enabled = _isHurt;

        if (!_isFlash)
        {
            if (_pushMovement > 0f)
            {
                _pushMovement -= _pushMovementStep;
                _pushMovementStep += 0.002f;
            }
            else
            {
                _pushMovement = 0f;
                _pushMovementStep = 0.002f;
            }
        }
        
    }

    void FixedUpdate()
    {
        if (_isFlash == false)
        {
            float horizontalVelocity = _movement.normalized.x * speed + _pushMovement * pushSpeed;
            _rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y);

            if (_isRocketing == true)
            {
                float rocketSpeed = skyline - this.transform.position.y;

                if (_rigidbody.velocity.y > rocketSpeed)
                {
                    rocketSpeed = _rigidbody.velocity.y;
                }

                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, rocketSpeed);

                rocket.GetComponent<AudioSource>().pitch = 3 - rocketSpeed / 40;
            }
        }
        else
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }

    void LateUpdate()
    {
        _animator.SetBool("Idle", _movement == Vector2.zero);
        _animator.SetBool("IsGrounded", _isGrounded);
        _animator.SetFloat("VerticalVelocity", _rigidbody.velocity.y);
        _animator.SetBool("IsRocketing", _isRocketing);
        _animator.SetBool("IsGameOver", _isGameOver);
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);

        backDust.transform.localScale = new Vector3(localScaleX, backDust.transform.localScale.y, backDust.transform.localScale.z);
        frontDust.transform.localScale = new Vector3(localScaleX, frontDust.transform.localScale.y, frontDust.transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Geyser")
        {
            Debug.Log("Colision");
            Hurt(new Vector2(50f, -100f));
            _waterSound.Play();
            _pushMovement = 1f;
        }
    }


    public void PlayWalkingDust()
    {
        backDust.Play();
        AudioSource stepAudio = backDust.GetComponent<AudioSource>();
        stepAudio.pitch = Random.Range(0.8f, 1.2f);
        stepAudio.Play();
    }

    public void Hurt(Vector2 hitDirection)
    {
        if (_isHurt == false)
        {
            if(!_isFlash)
            {
                StartCoroutine("FlashHurt", hitDirection);
            }
            
            _hitSound.Play();

        }
        else
        {
         
            if (!_isGameOver)
            {
                gameMusicController.StopMusic();

                if (!_isFlash)
                {
                    StartCoroutine("FlashGameOver");
                }
                StartCoroutine("HitSoundGameOver");
                               
            }
            
        }
            
    }

    public void Heal()
    {
        if (_isHurt == true)
        {
            _isHurt = false;
            _helmetUpSound.Play();
        }
    }

    IEnumerator HitSoundGameOver()
    {
        _hitSound.Play();
        yield return new WaitForSeconds(0.2f);
        _hitSound.pitch = _hitSound.pitch * 0.9f;
        _hitSound.Play();
        yield return new WaitForSeconds(0.2f);
        _hitSound.pitch = _hitSound.pitch * 0.9f;
        _hitSound.Play();
    }

    IEnumerator FlashHurt(Vector2 hitDirection)
    {
        _frontArmSprite.color = Color.black;
        _frontLegSprite.color = Color.black;
        _helmetSprite.color = Color.black;
        _headSprite.color = Color.black;
        _bodySprite.color = Color.black;
        _rocketSprite.color = Color.black;
        _backLegSprite.color = Color.black;
        _backArmSprite.color = Color.black;
        grass.color = Color.black;
        ground.color = Color.black;
        rock.color = Color.black;
        mineral.color = Color.black;

        flashBackgroundSprite.enabled = true;
               
        _animator.enabled = false;

        rocketController.ParticleStop();

        float oldGravity = _rigidbody.gravityScale;
        _rigidbody.gravityScale = 0f;

        _isFlash = true;

        yield return new WaitForSeconds(0.3f);

        _rigidbody.gravityScale = oldGravity;
        
        _animator.enabled = true;
        
        flashBackgroundSprite.enabled = false;

        _frontArmSprite.color = Color.white;
        _frontLegSprite.color = Color.white;
        _helmetSprite.color = Color.white;
        _headSprite.color = Color.white;
        _bodySprite.color = Color.white;
        _rocketSprite.color = Color.white;
        _backLegSprite.color = Color.white;
        _backArmSprite.color = Color.white;
        grass.color = Color.white;
        ground.color = Color.white;
        rock.color = Color.white;
        mineral.color = Color.white;

        _isFlash = false;

        _isHurt = true;

        if (lostHelmet)
        {
            GameObject instantiatedHelmet = Instantiate(helmetPrefab, new Vector3(this.transform.position.x + 1f, this.transform.position.y + 16f, 0f), Quaternion.identity) as GameObject;
            HelmetController instantiatedHelmetController = instantiatedHelmet.GetComponent<HelmetController>();
            instantiatedHelmetController.startMovement = new Vector2(hitDirection.x * 2f, hitDirection.y * -1.5f);
            instantiatedHelmetController.headTransform = _headTransform;
        }


    }

    IEnumerator FlashGameOver()
    {
        _frontArmSprite.color = Color.black;
        _frontLegSprite.color = Color.black;
        _helmetSprite.color = Color.black;
        _headSprite.color = Color.black;
        _bodySprite.color = Color.black;
        _rocketSprite.color = Color.black;
        _backLegSprite.color = Color.black;
        _backArmSprite.color = Color.black;
        grass.color = Color.black;
        ground.color = Color.black;
        rock.color = Color.black;
        mineral.color = Color.black;

        flashBackgroundSprite.enabled = true;

        _animator.enabled = false;

        rocketController.ParticleStop();

        float oldGravity = _rigidbody.gravityScale;
        _rigidbody.gravityScale = 0f;

        _isFlash = true;

        yield return new WaitForSeconds(1f);

        

        _rigidbody.gravityScale = oldGravity;

        _animator.enabled = true;

        flashBackgroundSprite.enabled = false;

        _frontArmSprite.color = Color.white;
        _frontLegSprite.color = Color.white;
        _helmetSprite.color = Color.white;
        _headSprite.color = Color.white;
        _bodySprite.color = Color.white;
        _rocketSprite.color = Color.white;
        _backLegSprite.color = Color.white;
        _backArmSprite.color = Color.white;
        grass.color = Color.white;
        ground.color = Color.white;
        rock.color = Color.white;
        mineral.color = Color.white;

        _isFlash = false;

        _isGameOver = true;
        gameOverMenu.GameOver();
        pauseMenu.GameOver();
    }

    public bool IsGameOver()
    {
        return _isGameOver;
    }

}
