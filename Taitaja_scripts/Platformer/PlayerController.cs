using UnityEngine;

public class PlatformerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    public float jumpForce = 10f; // Jump force
    public float gravityScale = 3f; // Adjust gravity to make jumps feel natural
    public float groundCheckDistance = 1f; // Distance for ground check raycast
    public float jumpGraceTime = 0.2f; // Time window for jump grace period
    public LayerMask groundLayer; // Layer that represents the ground

    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private float jumpGraceTimer = 0f; // Timer for jump grace period
    public bool isGrounded; // Whether the player is grounded or not

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        HandleMovement(); // Handle movement input
        CheckGrounded(); // Check if the player is grounded

        // Jump grace time logic
        if (jumpGraceTimer > 0)
        {
            jumpGraceTimer -= Time.deltaTime; // Decrease the grace timer
        }
    }

    void HandleMovement()
    {
        // Get horizontal input (A/D or Left/Right Arrow)
        float horizontal = Input.GetAxisRaw("Horizontal");

        // Apply movement using AddForce (avoiding direct velocity manipulation)
        Vector2 moveDirection = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        rb.AddForce(new Vector2(moveDirection.x, 0), ForceMode2D.Force); // Apply movement force without modifying vertical velocity directly

        // Jump logic
        if (isGrounded || jumpGraceTimer > 0) // If grounded or jump grace time is still valid
        {
            if (Input.GetButtonDown("Jump")) // Detect Jump (space bar or assigned button)
            {
                Jump();
            }
        }
    }

    void Jump()
    {
        // Reset the vertical velocity to ensure a consistent jump
        rb.velocity = new Vector2(rb.velocity.x, 0); // Clear any vertical velocity

        // Apply jump force (using AddForce for consistent jump behavior)
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        // Start the jump grace period
        jumpGraceTimer = 0f;
    }

    void CheckGrounded()
    {
        // Raycast to check if grounded
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);

        // If the player is in the air, start the grace period for jumping
        if (isGrounded)
        {
            jumpGraceTimer = jumpGraceTime; // Give a small grace window to jump
        }
    }
}
