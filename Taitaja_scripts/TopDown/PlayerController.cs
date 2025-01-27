using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    public float rotationSpeed = 700f; // Rotation speed
    public Camera mainCamera; // Reference to the camera for directional movement
    public bool canRotate = true; // If true, character rotates to face movement direction

    private Vector2 moveDirection; // Direction the player is moving towards
    private Rigidbody2D rb; // Reference to the Rigidbody2D component for physics-based movement

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component on the character
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Automatically assign the main camera if not assigned
        }
    }

    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        HandleMovement(); // Handle the character movement
    }

    void HandleMovement()
    {
        // Get input from the player (WASD or Arrow Keys)
        float horizontal = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right Arrow
        float vertical = Input.GetAxisRaw("Vertical"); // W/S or Up/Down Arrow

        // Calculate movement direction in world space
        Vector2 forward = mainCamera.transform.up; // Forward is the camera's up direction in world space
        Vector2 right = mainCamera.transform.right; // Right is the camera's right direction in world space

        // Calculate the movement direction based on the input
        moveDirection = (forward * vertical + right * horizontal).normalized;

        // If rotation is not allowed, we make sure diagonal movement is normalized
        if (!canRotate && (horizontal != 0 && vertical != 0))
        {
            moveDirection = new Vector2(horizontal, vertical).normalized; // Normalize the input for consistent speed
        }

        // Move the character
        MoveCharacter();

        // Rotate the character smoothly if canRotate is true
        if (canRotate && moveDirection.magnitude >= 0.1f)
        {
            RotateCharacter(moveDirection);
        }
    }

    void MoveCharacter()
    {
        // Move the character using Rigidbody2D (this is physics-based movement)
        Vector2 movement = moveDirection * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    void RotateCharacter(Vector2 direction)
    {
        // Calculate the angle to rotate the character towards
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Smoothly rotate the character towards the direction it's moving
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
