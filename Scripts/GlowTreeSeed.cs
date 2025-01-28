using UnityEngine;

public class GlowTreeSeed : MonoBehaviour
{
    private PlayerBehaviour playerBehaviour;
    private GameObject player;
    private SeedSpawner seedSpawner; // Reference to the SeedSpawner script

    [System.Obsolete]
    private void Start()
    {
        seedSpawner = FindObjectOfType<SeedSpawner>();

        if (seedSpawner == null)
        {
            Debug.LogWarning("SeedSpawner not found in the scene.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {


        //pick up seed
        if (other.CompareTag("Player"))
        {
            playerBehaviour = other.GetComponent<PlayerBehaviour>();

            Debug.Log("Player entered the trigger area!");

            if (playerBehaviour != null)
            {
                // Add seed to the player
                playerBehaviour.seeds += 1;
            }

            // Call the spawn seed function before destroying this seed
            if (seedSpawner != null)
            {
                seedSpawner.SpawnSeedWithDelay();
            }

            // Destroy the current seed
            Destroy(gameObject);
        }
    }
}
