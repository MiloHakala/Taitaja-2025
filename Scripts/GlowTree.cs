using UnityEngine;

public class GlowTree : MonoBehaviour
{


    public bool isInside = false;
    public GameObject player;
    private PlayerHealth playerHealth;

    private PlayerBehaviour playerBehaviour;
    private GameObject currentEnemy; // enemy in range
    public GameObject bulletPrefab; // Bullet prefab
    public float bulletSpeed = 10f; // Bullet speed
    public float shootCooldown = 1f; // Cooldown between shots
    private float cooldownTimer = 0f; // Timer for shooting cooldown

    //timer fot halth adding
    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (currentEnemy != null)
        {
            
            // Update cooldown timer
            if (cooldownTimer > 0)
            {
                cooldownTimer -= Time.deltaTime; // Reduce the cooldown timer
            }
            else
            {
                ShootAtEnemy(); // Shoot at the enemy
                cooldownTimer = shootCooldown; // Reset the cooldown
            }
        }
        if (isInside)
        {
            if (playerBehaviour != null)
            {
                playerBehaviour.isInside = true;
            }
            
            if (playerHealth != null)
            {
                
                timer += Time.deltaTime;

                // Every 1 second, increase health by 2
                if (timer >= 1f)
                {
                    
                    timer = 0f; // Reset the timer after increasing health
                    playerHealth.Heal(1);
                    playerBehaviour.AddGlow(10);


                }


            }
        }
     
    }


    //Check if player is inside trees range
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("other:" + other);
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {

            Debug.Log("Player entered the trigger area!");
            isInside = true;
            if (player == null || playerBehaviour == null || playerHealth ==null)
            {
                player = other.gameObject;
                playerHealth = player.GetComponent<PlayerHealth>();
                playerBehaviour = player.GetComponent<PlayerBehaviour>();
                print(playerHealth);
            }
            
        }
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy entered the trigger area!");

            // If it's the first enemy or the current enemy is different, set the new enemy
            if (currentEnemy == null)
            {
                currentEnemy = other.gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the player is still inside the trigger area
        if (other.CompareTag("Player"))
        {
            
            isInside = false;
            if (playerBehaviour)
            {
                playerBehaviour.isInside = false;
            }
        }
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy exited the trigger area!");

            // If the enemy is the one currently being targeted, stop shooting at them
            if (other.gameObject == currentEnemy)
            {
                currentEnemy = null;
            }
        }

    }
    void ShootAtEnemy()
    {
        if (currentEnemy != null)
        {
            // Calculate direction to the enemy
            Vector2 direction = (currentEnemy.transform.position - transform.position).normalized;

            // Instantiate the bullet and apply force
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().direction = direction;
            bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(currentEnemy == null)
        {
            currentEnemy = other.gameObject;
        }
    }
}
