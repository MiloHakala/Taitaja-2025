using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform player; // Reference to the player
    public float speed = 3f; // Movement speed
    public float attackRadius = 2f; // Radius within which the enemy attacks
    public float damage = 10f; // Damage dealt to the player
    public float attackCooldown = 1f; // Time between attacks



    private float lastAttackTime; // Tracks time since the last attack

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (player == null) return;

        // Calculate the distance to the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRadius)
        {
            // Move toward the player if outside attack radius
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else
        {
            // Attack the player if within range and cooldown is over
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                AttackPlayer();
                lastAttackTime = Time.time;
            }
        }
    }

    void AttackPlayer()
    {
        // Call the player's TakeDamage function
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }
    
}
