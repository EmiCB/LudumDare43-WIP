using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private Rigidbody2D _rb2d;
    private Collider2D _cldr;
    private Animator _anim;

    [SerializeField]
    private float _speed = 2.0f;

    [SerializeField]
    public bool _isGoingRight;
    public bool _isGoingLeft;

    private Vector2 _angle = new Vector2(1, -1);
    private Vector2 _angle2 = new Vector2(-1, -1);
    Vector2 fwd;

    int layerMask = 1 << 8;

    void Start () {
        _rb2d = GetComponent<Rigidbody2D>();
        _cldr = GetComponent<CapsuleCollider2D>();
        _anim = GetComponent<Animator>();

        _isGoingLeft = true;
        _isGoingRight = false;
    }

    void Update () {
        Movement();
	}

    void Movement()
    {
        if(_isGoingRight)
        {
            fwd = transform.TransformDirection(_angle);
        }
        if(_isGoingLeft)
        {
            fwd = transform.TransformDirection(_angle2);
        }
        

        RaycastHit2D hit = Physics2D.Raycast(transform.position, fwd, Mathf.Infinity, layerMask);
        Debug.Log("Hit: " + hit.collider + " Point: " + hit.point);
        

        if(hit.collider != null)
        {
            if (_isGoingRight)
            {
                _rb2d.velocity = new Vector2(_speed, _rb2d.velocity.y);

            }
            if (_isGoingLeft)
            {
                _rb2d.velocity = new Vector2(-(_speed), _rb2d.velocity.y);
            }
        }
        else
        {
            if (_isGoingRight)
            {
                _isGoingLeft = true;
                _isGoingRight = false;

            }
            if (_isGoingLeft)
            {
                _isGoingLeft = false;
                _isGoingRight = true;
            }
        }
    }
}
