using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private float attackRange;
    [SerializeField] private float chasingRange;
    [SerializeField] private float moveSpeed;

    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;

    private Vector2 directionToPlayer;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        ChooseState();
    }

    private void ChooseState()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, _playerController.transform.position);

        if (distanceToPlayer < attackRange)
        {
            Attack();
        }
        else if (distanceToPlayer > attackRange && distanceToPlayer < chasingRange)
        {
            ChasingPlayer();
        }
        else
        {
            Idle();
        }
    }

    void ChasingPlayer()
    {
        if (transform.position.x < _playerController.transform.position.x)
        {
            directionToPlayer = _playerController.transform.position - transform.position;
            _rigidbody2D.velocity = new Vector2(directionToPlayer.x + moveSpeed, directionToPlayer.y);
            _spriteRenderer.flipX = false;
        }
        else
        {
            directionToPlayer = _playerController.transform.position - transform.position;
            _rigidbody2D.velocity = new Vector2(directionToPlayer.x - moveSpeed, directionToPlayer.y);
            _spriteRenderer.flipX = true;
        }

        _animator.Play("Run");
    }

    void Idle()
    {
        _rigidbody2D.velocity = new Vector2(0, 0);
        _animator.Play("Idle");
    }

    void Attack()
    {
        _animator.Play("Attack");
    }
}