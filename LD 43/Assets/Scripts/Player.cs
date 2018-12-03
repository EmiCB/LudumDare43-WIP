using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    private Rigidbody2D _rb2d;
    private Collider2D _cldr;
    private Animator _anim;

    public LayerMask ground;

    HealthSystem healthSystem = new HealthSystem(100);
    public HealthBar healthBar;

    //movement
    [SerializeField]
    private float _speed = 5.0f;    
    private float _moveHorizontal;

    //jump
    [SerializeField]
    private float _force = 4.0f;      
    [SerializeField]
    private float _jumpTime = 0.25f;
    private float _jumpTimeCounter;

    // bools
    [SerializeField]
    private bool _isGrounded;

    public bool _isGoingRight;
    public bool _isGoingLeft;

    // ability stuff
    public bool _isArmed;

    private bool _isSpeedBoostActive;
    private float _speedBoostTime = 5.0f;
    private float _speedBoostTimeCounter;
    private float _speedBoostMulitplier = 2.0f;
    private int _speedBoostCost = 10;

    // text
    public Text healthText;
    public Text speedBoostTimerText;

    void Start () {
        _rb2d = GetComponent<Rigidbody2D>();
        _cldr = GetComponent<BoxCollider2D>();
        _anim = GetComponent<Animator>();

        _jumpTimeCounter = _jumpTime;
        _speedBoostTimeCounter = _speedBoostTime;

        _isGrounded = true;

        _isGoingLeft = false;
        _isGoingRight = true;

        _isSpeedBoostActive = false;


        healthBar.Setup(healthSystem);
    }
	
	void Update () {
        Movement();
        Abilities();
        TextUpdater();

        //checks if player is on ground
        _isGrounded = Physics2D.IsTouchingLayers(_cldr, ground);

        // animation stuff
        _anim.SetFloat("Speed", Mathf.Abs(_rb2d.velocity.x));
        _anim.SetBool("IsGoingRight", _isGoingRight);
        _anim.SetBool("IsGoingLeft", _isGoingLeft);
    }

    void Movement()
    {
        _moveHorizontal = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.D))
        {
            _rb2d.velocity = new Vector2(_speed, _rb2d.velocity.y);
            _isGoingRight = true;
            _isGoingLeft = false;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _rb2d.velocity = new Vector2(-(_speed), _rb2d.velocity.y);
            _isGoingLeft = true;
            _isGoingRight = false;
        }

        //better jumping
        if (Input.GetKey(KeyCode.W))
        {
            if (_jumpTimeCounter > 0)
            {
                _rb2d.velocity = new Vector2(_rb2d.velocity.x, _force);
                _jumpTimeCounter -= Time.deltaTime;
            }
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            _jumpTimeCounter = 0;
        }
        if (_isGrounded)
        {
            _jumpTimeCounter = _jumpTime;
        }
    }

    // Abilities
    void Abilities()
    {
        SpeedBoost();
    }
    void SpeedBoost()
    {
        if(!_isSpeedBoostActive)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _isSpeedBoostActive = true;
                healthSystem.Damage(_speedBoostCost);
                _speed = _speed * _speedBoostMulitplier;
                _speedBoostTimeCounter = _speedBoostTime;
            }
        }
        if(_isSpeedBoostActive)
        {
            if (_speedBoostTimeCounter > 0)
            {
                _speedBoostTimeCounter -= Time.deltaTime;
            }
            if (_speedBoostTimeCounter < 0)
            {
                _speed = _speed / _speedBoostMulitplier;
                _isSpeedBoostActive = false;
                _speedBoostTimeCounter = _speedBoostTime;
            }
        }
    }
    void AOEAttack()
    {
        if(_isArmed)
        {
            
        }
    }

    // UI
    void TextUpdater()
    {
        healthText.text = "HP: " + (healthSystem.GetHealthPercent()*100) + "%";
        speedBoostTimerText.text = _speedBoostTimeCounter.ToString("f2");
    }

    // Collisions
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "enemy")
        {
            healthSystem.Damage(10);
        }
    }
}
