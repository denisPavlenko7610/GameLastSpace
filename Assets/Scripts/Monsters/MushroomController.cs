using PlayerLogic;
using UnityEngine;

namespace Monsters
{
    public class MushroomController : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private float _attackRange = 7f;
        [SerializeField] private float _chasingRange = 20f;
        [SerializeField] private float _moveSpeed;

        private Animator _animator;
        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;

        private Vector2 directionToPlayer;
        string _runAnimation = "Run";

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

            if (distanceToPlayer < _attackRange)
            {
                Attack();
            }
            else if (distanceToPlayer > _attackRange && distanceToPlayer < _chasingRange)
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
                _rigidbody2D.velocity = new Vector2(directionToPlayer.x + _moveSpeed, directionToPlayer.y);
                _spriteRenderer.flipX = false;
            }
            else
            {
                directionToPlayer = _playerController.transform.position - transform.position;
                _rigidbody2D.velocity = new Vector2(directionToPlayer.x - _moveSpeed, directionToPlayer.y);
                _spriteRenderer.flipX = true;
            }

            _animator.Play(_runAnimation);
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
}