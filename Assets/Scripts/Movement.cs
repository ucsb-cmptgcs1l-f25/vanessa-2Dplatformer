using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private InputSystemActions _inputActions;
    private Vector2 _moveInput;
    private Animator animator;
    private bool facingRight = true;
    private Jump jump;
    private void Awake()
    {
        _inputActions = new InputSystemActions();
        _inputActions.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _inputActions.Player.Move.canceled += ctx => _moveInput = Vector2.zero;
    }

    private void OnEnable()
    {
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }

    private void Start()
    {
        jump = GetComponent<Jump>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {  
        _moveInput = _inputActions.Player.Move.ReadValue<Vector2>();
        float horizontal = _moveInput.x;

        // Apply horizontal movement
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

        // Flip sprite
        if ((facingRight && horizontal < 0f) || (!facingRight && horizontal > 0f))
        {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1f;
            transform.localScale = scale;
        }

        // Animation
        bool isRunning = Mathf.Abs(horizontal) > 0.1f && jump.Grounded();
        animator.SetBool("isRunning", isRunning);
    }
    
}
