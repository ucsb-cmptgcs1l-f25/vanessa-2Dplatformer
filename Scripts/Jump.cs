using UnityEngine;

public class Jump : MonoBehaviour
{
    private Rigidbody2D rb;
    public LayerMask groundLayer;    
 
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    private bool isGrounded;
    public float jump;
    private Animator animator; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public bool Grounded()
    {
        return isGrounded;
    }


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
