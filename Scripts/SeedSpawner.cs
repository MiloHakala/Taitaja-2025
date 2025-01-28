using UnityEngine;

public class SeedSpawner : MonoBehaviour
{
    [Header("Seed Spawning Settings")]
    public GameObject seedPrefab; // seed prefab
    public float spawnRadius = 5f; // spawn radius
    public float spawnDelay = 5f; // Delay

    private GameObject player; // Reference to the player

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void SpawnSeed()
    {
        if (player == null)
        {
            Debug.LogWarning("Player not found. Seed cannot be spawned.");
            return;
        }

        // Generate a random position within the spawn radius
        Vector2 randomPosition = (Vector2)player.transform.position + Random.insideUnitCircle * spawnRadius;

        // Spawn the seed prefab
        Instantiate(seedPrefab, randomPosition, Quaternion.identity);

        Debug.Log("Seed spawned at: " + randomPosition);
    }

    public void SpawnSeedWithDelay()
    {
        StartCoroutine(SpawnSeedAfterDelay());
    }

    private System.Collections.IEnumerator SpawnSeedAfterDelay()
    {
        yield return new WaitForSeconds(spawnDelay);

        // Spawn the seed
        SpawnSeed();
    }
}
