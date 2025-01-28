using UnityEngine;

public class ShootingBehaviour : MonoBehaviour
{
    public Transform spawnPos;
    public Camera mainCamera;
    public GameObject cursor;
    public GameObject bulletPrefab; // Bullet prefab to spawn
    public float shootForce = 10f; // Force applied to the bullet

    Vector2 mouseWorldPos; // Cursor's current position (now a Vector2)

    void Update()
    {
        

        // Check for shooting input (e.g., left mouse click or fire button)
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        // Get the mouse world position
        mouseWorldPos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
        print("Mouse World Position: " + mouseWorldPos); // Debugging mouse position

        // Calculate the direction from the player to the mouse position
        Vector2 shootDirection = (mouseWorldPos - (Vector2)transform.position).normalized;
        print("Shoot Direction: " + shootDirection); // Debugging shoot direction

        // Spawn the bullet at the spawn position with the player's rotation
        GameObject bullet = Instantiate(bulletPrefab, spawnPos.position, transform.rotation);

        // Apply force to the bullet using Rigidbody2D
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            bullet.GetComponent<Bullet>().direction = shootDirection;
            rb.AddForce(shootDirection * shootForce, ForceMode2D.Impulse); // Apply the force
        }
        else
        {
            print("Rigidbody2D is missing on bullet!");
        }

    }

}
