using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnRate = 2.0f;
    [SerializeField] private int numberOfSpawn = 15;

    private void Awake()
    {
        StartCoroutine(SpawnEnemy());
    }

    public void EnemySpawned()
    {
        GameManager.Instance.EnemySpawned();
    }

    public void EnemyDestroyed()
    {
        GameManager.Instance.EnemyDestroyed();
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitUntil(() => GameManager.Instance != null);

        int spawnCount = 0;

        while (!GameManager.Instance.isGameOver && spawnCount < numberOfSpawn)
        {
            if (enemyPrefab != null)
            {
                GameObject enemyInstance = Instantiate(enemyPrefab, transform.position, transform.rotation);
                enemyInstance.SetActive(true);
                GameManager.Instance.EnemySpawned();
                spawnCount++;
                yield return new WaitForSeconds(spawnRate);
            }
            else
                yield break ;
        }
    }
}
