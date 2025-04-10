using System.Collections;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Vector2 min;
    [SerializeField] protected Vector2 max;
    [SerializeField] private GameObject enemyPrefab;

    private List<GameObject> enemies = new List<GameObject>();

    public float spawnInterval = 1f;
    public int maxEnemies = 20;
    public bool doRespawn = false;

    public int NbLiving { get; private set; } = 0;
    public int NbSpawnedInWave { get; private set; } = 0;
    public int NbSpawnedInGame { get; private set; } = 0;

    private Coroutine coroutine = null;

    public int GetTotalEnemiesKilled()
    {
        return NbSpawnedInWave;
    }

    public void SpawnEnemies() 
    {
        coroutine = StartCoroutine(SpawnEnemy(spawnInterval, enemyPrefab));
    }

    public void StartNewWave()
    {
        if (coroutine != null) StopCoroutine(coroutine);
        NbLiving = 0;
        NbSpawnedInWave = 0;
        SpawnEnemies();

    }

    public void StopSpawning()
    {
        StopCoroutine(coroutine);
    }

    public bool NotifyDestroy(GameObject enemy)
    {
        if(!enemy.CompareTag("Enemy")) throw new System.Exception("wrong GameObject, on attendait le tag prefab");
        
        if(!enemies.Remove(enemy)) return false; // on vérifie que l'ennemi a été créé par le spawner
        Destroy(enemy);
        NbLiving--;
        return true;
    }

    private IEnumerator SpawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        if (NbSpawnedInWave < maxEnemies || (doRespawn && NbLiving < maxEnemies))
        {
            GameObject obj = Instantiate(enemyPrefab, new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), 0), Quaternion.identity);
            enemies.Add(obj);
            NbLiving++;
            NbSpawnedInWave++;
            NbSpawnedInGame++;
            GameManager.Instance.ShowProgress();
        }
        StartCoroutine(SpawnEnemy(interval, enemy));
    }
}
