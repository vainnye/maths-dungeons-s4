using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Vector2 min;
    [SerializeField] protected Vector2 max;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval;

    [SerializeField] private int maxEnemies = 5;
    private int count = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemy(spawnInterval, enemyPrefab));
    }

    private IEnumerator SpawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        if(count < maxEnemies)
        {
            Instantiate(enemyPrefab, new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), 0), Quaternion.identity);
            count++;
        }
        StartCoroutine(SpawnEnemy(interval, enemy));
    }
}
