
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    public List<GameObject> enemiesInRange = new List<GameObject>();

    // Stats
    public float damages = 0.1f;
    public float fireRates = 0.1f;
    public int cost = 1;

    // Utilities
    private GameObject closestEnemy = null;
    private float actualDistance = float.MaxValue;
    private float distance = 0.1f;
    private float fireRateTimer = 0;

    private void Update()
    {
        if (fireRateTimer < fireRates)
            fireRateTimer += Time.deltaTime;
        if (enemiesInRange.Count > 0 && fireRateTimer >= fireRates)
        {
            CheckTarget();
            fireRateTimer = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            enemiesInRange.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            enemiesInRange.Remove(other.gameObject);
    }

    private void CheckTarget()
    {
        foreach (GameObject enemy in enemiesInRange)
        {
            distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < actualDistance)
            {
                actualDistance = distance;
                closestEnemy = enemy;
            }
        }
        if (closestEnemy != null)
        {
            actualDistance = float.MaxValue;
            launchBullet(closestEnemy.transform.position - transform.position);
        }
    }

    private void launchBullet(Vector2 direction)
    {
        EnemyController enemyController = closestEnemy.GetComponent<EnemyController>();
        float enemySpeed = enemyController.currentSpeed;
        Vector2 enemyVelocity = enemyController.rb.velocity;
        Vector2 enemyPosition = closestEnemy.transform.position;
        float timeToHit = direction.magnitude / bulletPrefab.GetComponent<BulletController>().moveSpeed;
        Vector2 predictedPosition = enemyPosition + enemyVelocity * timeToHit;
        direction = predictedPosition - (Vector2)transform.position;

        GameObject bulletInstance = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bulletInstance.SetActive(true);
        bulletInstance.GetComponent<BulletController>().SetDamages(damages);
        bulletInstance.GetComponent<BulletController>().Launch(direction);
    }
}