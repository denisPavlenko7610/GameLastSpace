using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Player parameters")] public int speed = 0;
    [SerializeField] private float jumpForce = 150f;

    [Header("Ground check")] [SerializeField]
    private LayerMask groundLayer;

    [SerializeField] private Transform groundCheck;

    [Header("Joystick controller")] [SerializeField]
    JoystickButton _jumpButton;

    protected Joystick _joystick;

    private bool isGrounded = false;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private float _horizontalMove;
    private float _verticalMove;

    private AudioSource _audioSource;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _joystick = FindObjectOfType<Joystick>();
    }

    private void Update()
    {
        CheckIsGround();

        CheckMove();

        CheckJump();

        CheckAttack();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            _rigidbody2D.gravityScale = 0;
            ClimbOnLadder();
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
        _horizontalMove = Input.GetAxisRaw("Horizontal");
        if (math.abs(_horizontalMove) > 0)
        {
            _animator.SetFloat("Speed", math.abs(_horizontalMove));
        }
        else if (math.abs(_joystick.Horizontal) > 0)
        {
            _animator.SetFloat("Speed", math.abs(_joystick.Horizontal));
        }
        else
        {
            _animator.SetFloat("Speed", 0);
        }
    }

    private void CheckJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded || _jumpButton.pressed && isGrounded)
        {
            _animator.SetBool("IsJump", true);
            JumpPlayer();
        }
        else
        {
            _animator.SetBool("IsJump", false);
        }
    }

    private void CheckAttack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //Ignore click through UI
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            _audioSource.Play();
            _animator.SetBool("IsAttack", true);
        }
        else
        {
            _animator.SetBool("IsAttack", false);
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void JumpPlayer()
    {
        if (_jumpButton.pressed)
        {
            _rigidbody2D.AddForce(transform.up * (jumpForce / 3));
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
            new Vector2(_horizontalMove * speed * Time.fixedDeltaTime + _joystick.Horizontal * speed * Time.deltaTime,
                _rigidbody2D.velocity.y);
    }

    private void ClimbOnLadder()
    {
        _verticalMove = Input.GetAxisRaw("Vertical");

        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x,
            _verticalMove * speed * Time.deltaTime + _joystick.Vertical * speed * Time.deltaTime);
    }
}