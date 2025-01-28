using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerBehaviour : MonoBehaviour
{
    public GameObject enemyPrefab; // Reference to the enemy prefab
    public int numberOfEnemies = 3; // Number of enemies to spawn
    public float spawnRadius = 5f; // Radius around the player within which enemies will spawn


    [Header("Player Movement")]
    public float moveSpeed = 5f; // player Speed
    private Animator animator; // Reference to the Animator component
    private Rigidbody2D rb;
    private Vector2 movement;

    [Header("Seeds & Trees")]
    public int treesPlanted = 1;
    public GameObject firstTree;
    private List<Vector2> treePos = new List<Vector2>();
    public int seeds = 1;
    public GameObject treePrefab;
    public int maxGlow = 100;
    public int currentGlow = 0;
    public GameDialogue dialogueScript;

    // environment audio
    public AudioSource Wind;

    

    public Light2D GlobalLight;

    private float spawntimer = 0;

    public bool isInside = false;
    void Start()
    {
        Wind.volume = 0.4f;
        treePos.Add(firstTree.transform.position);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    void Update()
    {
        if (!isInside && GlobalLight.intensity != 0f)
        {
            Wind.volume = Mathf.Lerp(Wind.volume, 0.4f, 4f * Time.deltaTime);
            spawntimer += Time.deltaTime;
            if(spawntimer > 3)
            {
                spawntimer = 0;
                SpawnEnemies();
            }
            
            GlobalLight.intensity = Mathf.Lerp(GlobalLight.intensity, 0f, 4f * Time.deltaTime);
        }
        else if(isInside && GlobalLight.intensity != 0.3f)
        {
            Wind.volume = Mathf.Lerp(Wind.volume, 0.1f, 4f * Time.deltaTime);
            GlobalLight.intensity = Mathf.Lerp(GlobalLight.intensity, 0.3f, 8f * Time.deltaTime);
        }
        // Get input from WASD or arrow keys (using Unity's old input system, change if using Input System package)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized; // Prevent faster diagonal movement

        // Update animation state based on movement
        animator.SetBool("is_walking", movement.x != 0 || movement.y != 0);

        // Check if the player pressed the "E" key to plant a tree
        if (Input.GetKeyDown(KeyCode.E)) // 'E' key for planting
        {
            if (seeds > 0)
            {
                Plant();
            }
        }
       
    }

    // Add glow (to be used later or connected to UI)
    public void AddGlow(int amount)
    {
        currentGlow += amount;
        currentGlow = Mathf.Clamp(currentGlow, 0, maxGlow);
    }

    void FixedUpdate()
    {
        // Move the player with Rigidbody2D
        rb.linearVelocity = movement * moveSpeed;
    }

    public void Plant()
    {
        if (checkNearestTree())
        {
            animator.SetTrigger("plant");
            treesPlanted += 1;
            maxGlow += 100;
            SpawnTree();

            if(treesPlanted == 10)
            {
                dialogueScript.youWon();
                //win
            }
        }
        else
        {
            dialogueScript.DisplayTextWithFade("Too close or Too far from other trees");
        }
    }

    // Check if the nearest tree is too close to plant a new tree
    public bool checkNearestTree()
    {
        for (int i = 0; i < treePos.Count; i++)
        {
            float distance = Vector2.Distance(transform.position, treePos[i]);
            if (distance < 2f) // Check if the distance is smaller than 2 units (you can adjust this value)
            {
                print("Not okay to plant, too close to another tree.");
                return false; // Too close to another tree
            }
            else if(distance > 5f)
            {
                print("too faar from other trees");
                return false;
            }
        }
        print("Okay to plant.");
        return true;
    }

    void SpawnTree()
    {
        seeds -= 1;
        // Spawn the prefab at the player's position
        Instantiate(treePrefab, transform.position, Quaternion.identity);
        treePos.Add(transform.position); // Store the tree's position
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Get a random position within the specified radius around the player
            Vector2 randomPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;

            // Instantiate the enemy at the random position
            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        }
    }

}
