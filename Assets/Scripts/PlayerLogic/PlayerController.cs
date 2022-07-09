using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayerLogic
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Player parameters")] public int speed;
        [SerializeField] private float jumpForce = 150f;

        [Header("Ground check")] [SerializeField]
        private LayerMask groundLayer;

        [SerializeField] private Transform groundCheck;

        [Header("Joystick controller")] [SerializeField]
        JoystickButton _jumpButton;

        protected Joystick _joystick;

        private bool isGrounded;
        private Rigidbody2D _rigidbody2D;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private float _horizontalMove;
        private float _verticalMove;
        private AudioSource _audioSource;
    
        string _ladderTag = "Ladder";
        private readonly int _speed = Animator.StringToHash("Speed");
        private readonly int _isJump = Animator.StringToHash("IsJump");
        private static readonly int _isAttack = Animator.StringToHash("IsAttack");

        void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _audioSource = GetComponent<AudioSource>();
            _joystick = FindObjectOfType<Joystick>();
        }

        private void Update()
        {
            CheckIsGround();

            CheckMove();

            CheckJump();

            CheckAttack();
        }

        void FixedUpdate()
        {
            MovePlayer();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(_ladderTag))
            {
                _rigidbody2D.gravityScale = 0;
                ClimbOnLadder();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag(_ladderTag))
                return;

            var gravityScale = 3;
            _rigidbody2D.gravityScale = gravityScale;
        }

        private void CheckIsGround()
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15f, groundLayer);
        }

        private void CheckMove()
        {
            var axisName = "Horizontal";
            _horizontalMove = Input.GetAxisRaw(axisName);
            if (math.abs(_horizontalMove) > 0)
            {
                _animator.SetFloat(_speed, math.abs(_horizontalMove));
            }
            else if (math.abs(_joystick.Horizontal) > 0)
            {
                _animator.SetFloat(_speed, math.abs(_joystick.Horizontal));
            }
            else
            {
                _animator.SetFloat(_speed, 0);
            }
        }

        private void CheckJump()
        {
            var buttonName = "Jump";
            if (Input.GetButtonDown(buttonName) && isGrounded || _jumpButton.pressed && isGrounded)
            {
                _animator.SetBool(_isJump, true);
                JumpPlayer();
            }
            else
            {
                _animator.SetBool(_isJump, false);
            }
        }

        private void CheckAttack()
        {
            var fireButton = "Fire1";
            if (Input.GetButtonDown(fireButton))
            {
                if (IgnoreClickThroughUI())
                    return;

                _audioSource.Play();
                _animator.SetBool(_isAttack, true);
            }
            else
            {
                _animator.SetBool(_isAttack, false);
            }
        }

        private bool IgnoreClickThroughUI()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return true;
            }

            return false;
        }

        private void JumpPlayer()
        {
            if (_jumpButton.pressed)
            {
                var divider = 3;
                _rigidbody2D.AddForce(transform.up * (jumpForce / divider));
            }
            else
            {
                _rigidbody2D.AddForce(transform.up * jumpForce);
            }
        }

        private void MovePlayer()
        {
            if (_horizontalMove > 0 || _joystick.Horizontal > 0)
            {
                _spriteRenderer.flipX = false;
            }
            else if (_horizontalMove < 0 || _joystick.Horizontal < 0)
            {
                _spriteRenderer.flipX = true;
            }

            _rigidbody2D.velocity =
                new Vector2(_horizontalMove * speed * Time.deltaTime + _joystick.Horizontal * speed * Time.deltaTime,
                    _rigidbody2D.velocity.y);
        }

        private void ClimbOnLadder()
        {
            var verticalAxis = "Vertical";
            _verticalMove = Input.GetAxisRaw(verticalAxis);

            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x,
                _verticalMove * speed * Time.deltaTime + _joystick.Vertical * speed * Time.deltaTime);
        }
    }
}