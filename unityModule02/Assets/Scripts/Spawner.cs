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
        while (GameManager.Instance != null && !GameManager.Instance.isGameOver)
        {
            if (enemyPrefab != null)
            {
                GameObject enemyInstance = Instantiate(enemyPrefab, transform.position, transform.rotation);
                enemyInstance.SetActive(true);
                yield return new WaitForSeconds(spawnRate);
            }
            else
                yield break ;
        }
    }
}
