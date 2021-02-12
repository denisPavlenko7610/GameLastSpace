using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int speed = 0;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private float movementDirection;
    private float moveBy;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        movementDirection = Input.GetAxis("Horizontal");

        moveBy = movementDirection * speed;
        if (movementDirection > 0.1f)
        {
            _animator.SetFloat("Speed", movementDirection);
            _spriteRenderer.flipX = false;
        }
        else if (movementDirection < -0.1f)
        {
            _animator.SetFloat("Speed", -movementDirection);
            _spriteRenderer.flipX = true;
        }

        _rigidbody2D.velocity = new Vector2(moveBy, _rigidbody2D.velocity.y);
    }
}