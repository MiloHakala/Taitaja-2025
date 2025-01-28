using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public Light2D playerLight;

    public Slider healthBar;

    public GameDialogue dialogueScript;

    private bool dead = false;
    private float timer = 0;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    void Update()
    {
        if (dead)
        {
            timer += Time.deltaTime;
            if (timer > 2)
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10f); // Simulate damage for testing
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.value = currentHealth;
    }

    private void Die()
    {
        Debug.Log("Player Died");
        playerLight.intensity = 0;
        dialogueScript.DisplayTextWithFade("You died :(");
        dead = true;


        // Additional logic for player death
    }
}
