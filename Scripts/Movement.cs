
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool facingRight = true;
    private Jump jump;

    public float mushroomSpeed = 7f;
    public float woodSpeed = 3f;
    public float bushSpeed = 4f;
    public float iceshard = 7f;
    public float glacier = 3f;
    public float icechunk = 4f;

    private string carryingType = "none";

    private void Start()
    {
        jump = GetComponent<Jump>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontal = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))
            horizontal = -1f;
        else if (Input.GetKey(KeyCode.RightArrow))
            horizontal = 1f;

        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

        if ((facingRight && horizontal < 0f) || (!facingRight && horizontal > 0f))
        {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1f;
            transform.localScale = scale;
        }

        bool isRunning = Mathf.Abs(horizontal) > 0.1f && jump.Grounded();
        animator.SetBool("isRunning", isRunning);
    }
    public void SetCarryingType(string type)
    {
        carryingType = type;

        switch (type)
        {
            case "Mushroom":
                speed = mushroomSpeed;
                break;

            case "Wood":
                speed = woodSpeed;
                break;

            case "Bush":
                speed = bushSpeed;
                break;
            case "icechunk":
                speed = icechunk;
                break;
            case "glacier":
                speed = glacier;
                break;
            case "iceshard":
                speed = iceshard;
                break;
            default:
                speed = 5f;  
                break;
        }
    }
}