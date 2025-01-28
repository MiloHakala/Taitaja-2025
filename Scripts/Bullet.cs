using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLifeTime = 5f; // Time before the bullet is destroyed
    public float damage = 10f; // Bullet damage
    public float bulletSpeed = 10f; // Bullet speed

    private Rigidbody2D rb; // Rigidbody2D component
    public Vector2 direction; // Bullet direction

    private void Start()
    {
        // Get the Rigidbody2D component attached to the bullet
        rb = GetComponent<Rigidbody2D>();

        // Get the direction based on the bullet's current rotation (assuming the bullet is fired in the direction it's facing)
       // direction = transform.right; // Use transform.right for the right-facing direction of the bullet

        // Set the velocity of the bullet immediately
        rb.linearVelocity = direction * bulletSpeed;

        // Destroy the bullet after its lifetime
        Destroy(gameObject, bulletLifeTime);
    }

    private void Update()
    {
        // Optionally, keep updating the linear velocity in case of changes in the direction
        // This is mostly useful if you need to adjust the direction dynamically
        // Here, it's redundant unless you have complex trajectory logic
        rb.linearVelocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the bullet collides with an object tagged as "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Call the TakeDamage method on the enemy's health component
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);

            // Destroy the bullet upon impact
            Destroy(gameObject);
        }
    }
}
