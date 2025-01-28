using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawners; // Array of spawn points
    public GameObject enemyPrefab; // The enemy prefab to spawn
    public float spawnCooldown = 2f; // Time between spawns

    private float spawnTimer; // Tracks the time since the last spawn
    bool isSpawning;
    public int maxEnemies = 8;
    public int currentEnemies;

    private void Start()
    {
        currentEnemies = 0; // Initialize enemy count
    }

    void Update()
    {
        // Only spawn a new enemy if the cooldown has passed and we haven't reached max enemies
        if (Time.time >= spawnTimer + spawnCooldown && currentEnemies < maxEnemies)
        {
            SpawnEnemy();
            spawnTimer = Time.time; // Reset the spawn timer
        }
    }

    void SpawnEnemy()
    {
        if (spawners.Length == 0) return; // No spawn points available

        // Choose a random spawn point from the array
        Transform randomSpawnPoint = spawners[Random.Range(0, spawners.Length)];

        // Instantiate the enemy at the selected spawn point
        Instantiate(enemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);

        // Increment the number of current enemies after spawning
        currentEnemies += 1;
    }

    // Call this function from somewhere to decrement enemy count when an enemy is destroyed
    public void DecrementEnemyCount()
    {
        currentEnemies = Mathf.Max(0, currentEnemies - 1);
    }
}
