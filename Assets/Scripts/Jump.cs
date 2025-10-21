using UnityEngine;

public class Jump : MonoBehaviour
{
    private Rigidbody2D rb;
    public LayerMask groundLayer; // Layer(s) considered as ground
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    private bool isGrounded;
    public float jump;
    private Animator animator; // Animator reference

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public bool Grounded()
    {
        return isGrounded;
    }


// Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector2(rb.linearVelocity.x, jump));
        }

        if (animator != null)
        {
            animator.SetBool("isJumping", !isGrounded);
        }
}
}
