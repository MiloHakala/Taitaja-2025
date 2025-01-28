using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 50f; // Maximum health of the enemy
    private float currentHealth;
    private Animator animator; // Reference to the Animator component

    private Rigidbody2D rb; // Reference to the Rigidbody2D component

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D attached to the enemy
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        
    }

    public void TakeDamage(float damage)
    {

        // Reduce health
        currentHealth -= damage;

        // Check if the enemy is dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(20);
            
        }
    }
    void Die()
    {
        animator.SetTrigger("die");
        Debug.Log("Enemy Died");
        Destroy(gameObject, 0.75f); // Destroy the enemy GameObject
    }
}
