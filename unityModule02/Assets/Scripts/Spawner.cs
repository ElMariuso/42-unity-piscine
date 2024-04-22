using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnRate = 2.0f;

    private IEnumerator routine = null;

    private void Awake()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        if (enemyPrefab != null)
        {
            GameObject enemyInstance = Instantiate(enemyPrefab, transform.position, transform.rotation);
            enemyInstance.setActive(true);
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
