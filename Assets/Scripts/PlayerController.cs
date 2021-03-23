using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed = 0;
    [SerializeField] private float jumpForce = 150f;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    bool isGrounded = false;

//Private fields
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private float _horizontalMove;
    private float _verticalMove;
    private bool _isJump = false;

    private AudioSource _audioSource;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        CheckIsGround();

        CheckMove();

        CheckJump();

        CheckAttack();

        CheckBlock();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            _rigidbody2D.gravityScale = 0;
            Climb();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            _rigidbody2D.gravityScale = 3;
        }
    }

    private void CheckIsGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15f, groundLayer);
    }

    private void CheckMove()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        _animator.SetFloat("Speed", math.abs(_horizontalMove));
    }

    private void CheckJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            _isJump = true;
            _animator.SetBool("IsJump", true);
            JumpPlayer();
        }
        else
        {
            _isJump = false;
            _animator.SetBool("IsJump", false);
        }
    }

    private void CheckAttack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _audioSource.Play();
            _animator.SetBool("IsAttack", true);
        }
        else
        {
            _animator.SetBool("IsAttack", false);
        }
    }

    private void CheckBlock()
    {
        if (Input.GetButton("Fire2"))
        {
            _animator.SetBool("IsBlock", true);
        }
        else
        {
            _animator.SetBool("IsBlock", false);
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void JumpPlayer()
    {
        _rigidbody2D.AddForce(transform.up * jumpForce);
        _isJump = false;
    }

    private void MovePlayer()
    {
        if (_horizontalMove > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (_horizontalMove < 0)
        {
            _spriteRenderer.flipX = true;
        }

        _rigidbody2D.velocity = new Vector2(_horizontalMove * Time.fixedDeltaTime, _rigidbody2D.velocity.y);
    }

    private void Climb()
    {
        _verticalMove = Input.GetAxisRaw("Vertical");
        
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _verticalMove * speed * Time.deltaTime);
    }
}