using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int timeBeforeLaunch = 3;
    [SerializeField] private float spawnRate = 2.0f;
    public int numberOfSpawn = 15;

    private void Awake()
    {
        Spawner sp = GetComponent<Spawner>();
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetupSpawner(sp);  
        }

        StartCoroutine(SpawnEnemy());
    }

    public void EnemySpawned()
    {
        GameManager.Instance.EnemySpawned();
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitUntil(() => GameManager.Instance != null);
        yield return new WaitForSecondsRealtime(timeBeforeLaunch);

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
