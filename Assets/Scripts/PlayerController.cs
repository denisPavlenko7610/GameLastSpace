using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int speed = 0;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private float horizontalMove;
    [SerializeField] private float jumpForce = 150f;
    private bool isJump = false;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        _animator.SetFloat("Speed", math.abs(horizontalMove));
        
        if (Input.GetButtonDown("Jump"))
        {
            isJump = true;
        }
        else
        {
            isJump = false;
        }
    }

    void FixedUpdate()
    {
        MovePlayer();

        if (isJump)
        {
            _animator.SetBool("IsJump", true);
            JumpPlayer();
        }
        else
        {
            _animator.SetBool("IsJump", false);
        }
        
    }

    private void JumpPlayer()
    {
        _rigidbody2D.AddForce(transform.up * jumpForce);
        isJump = false;
    }

    private void MovePlayer()
    {
        if (horizontalMove > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (horizontalMove < 0)
        {
            _spriteRenderer.flipX = true;
        }

        _rigidbody2D.velocity = new Vector2(horizontalMove * Time.fixedDeltaTime, _rigidbody2D.velocity.y);
    }
}