using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float damages = 0.1f;
    public float moveSpeed = 5f;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetDamages(float newDamages)
    {
        damages = newDamages;
    }

    public void Launch(Vector2 direction)
    {
        if (rb != null)
            rb.velocity = direction.normalized * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            enemy.TakeDamage(damages);
            DestroyBullet();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("MapLimits"))
            DestroyBullet();
    }

    private void DestroyBullet() { Destroy(gameObject); }
}
