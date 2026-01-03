using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D _rb;

    [SerializeField] float _speed;
    [SerializeField] float _turnForce;
    [SerializeField] float _maxSpeed;
    [SerializeField] float _regGrav;

    [SerializeField] float _moveDamping;
    [SerializeField] float _stillDamping;
    [SerializeField] float _airDamping;

    [SerializeField] float _jumpForce;
    [SerializeField] float _jumpHighGrav;
    [SerializeField] float _jumpLowGrav;

    [SerializeField] Transform _groundCheckCircle;
    [SerializeField] Transform _largeGroundCheckCircle;
    [SerializeField] LayerMask _groundLayer;

    [SerializeField] float _coyoteTime;
    [SerializeField] float _jumpBufferTime;
    float _coyoteTimer;
    float _jumpBufferTimer;

    [SerializeField] float _bounceForce;

    [SerializeField] float _smallCrouchSize;
    float _regularYSize;

    bool _isRight;
    Vector2 _regularScale;

    public int keyCount;

    float _xMove;
    bool _isJumping;
    public bool canMove;

    private void Awake()
    {
        _regularYSize = transform.localScale.y;

        _rb = GetComponent<Rigidbody2D>();
        canMove = true;
    }

    private void Start()
    {
        _regularScale = transform.localScale;

        keyCount = 0;
    }

    private void Update()
    {

        GetInput();
        if (canMove)
        {
            Move();
            if (_isJumping && IsGrounded()) { Jump(); }
        }

        if (_rb.linearVelocityY > 0 && Input.GetKeyUp(KeyCode.Space))
        {
            _rb.gravityScale = _jumpHighGrav;
        }

        Crouch();

        SetGrav();
        SetDamping();

        Flip();
    }

    private void Crouch()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.localScale = new Vector2(transform.localScale.x, _smallCrouchSize);
        }
        else
        {
            transform.localScale = new Vector2(transform.localScale.x, _regularYSize);
        }
    }

    private void SetGrav()
    {
        if (IsGrounded())
        {
            _rb.gravityScale = _regGrav;
        } else if (Mathf.Approximately(_rb.linearVelocityY, 0.5f) && !IsGrounded())
        {
            _rb.gravityScale = _jumpLowGrav;
        } else if (_rb.linearVelocityY < 0)
        {
            _rb.gravityScale = _jumpHighGrav;
        }
    }

    private void SetDamping()
    {
        if (!IsGrounded()) { _rb.linearDamping = _airDamping; }
        else if (_xMove != 0) { _rb.linearDamping = _moveDamping; }
        else { _rb.linearDamping = _stillDamping; ; }
    }

    private void GetInput()
    {
        _xMove = Input.GetAxisRaw("Horizontal");
        _isJumping = Input.GetKeyDown(KeyCode.Space);
    }

    private void Move()
    {
        if (Mathf.Sign(_xMove) != Mathf.Sign(_rb.linearVelocityX))
        {
            _rb.AddForceX(_xMove * _turnForce);
        }
        else
        {
            _rb.AddForceX(_xMove * _speed);
        }

        if (_rb.linearVelocityX > _maxSpeed) _rb.linearVelocityX = _maxSpeed;
        if (_rb.linearVelocityX < -_maxSpeed) _rb.linearVelocityX = -_maxSpeed;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheckCircle.position, 0.1f, _groundLayer);
    }

    bool IsAlmostGrounded()
    {
        return Physics2D.OverlapCircle(_largeGroundCheckCircle.position, 0.1f, _groundLayer);
    }

    private void Jump()
    {
        _rb.linearVelocityY = _jumpForce;
    }


    private void Flip()
    {
        if (_isRight && _rb.linearVelocityX < -1) _isRight = false;
        if (!_isRight && _rb.linearVelocityX > 1) _isRight = true;

        if (_isRight) { transform.localScale = new Vector2(_regularScale.x, _regularScale.y); }
        else if (!_isRight) { transform.localScale = new Vector2(-_regularScale.x, _regularScale.y); }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bounce"))
        {
            _rb.AddForceY(_bounceForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Key"))
        {
            keyCount++;
            Destroy(collision.gameObject);
        }
    }

    public void AddVel(Vector2 vel)
    {
        _rb.position += vel;
    }
}
