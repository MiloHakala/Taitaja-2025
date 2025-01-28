using UnityEngine;

public class enemyTriggerArea : MonoBehaviour
{
    private EnemyBehaviour parentEnemy;


    private void Start()
    {
        parentEnemy = GetComponentInParent<EnemyBehaviour>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            parentEnemy.player = other.gameObject.transform;
        }
    }
}
