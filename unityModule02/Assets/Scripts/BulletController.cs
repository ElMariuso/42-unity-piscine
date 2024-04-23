using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float damages = 0.1f;

    private void SetDamages(float newDamages)
    {
        damages = newDamages;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            enemy.TakeDamage(damages);
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "MapLimits")
            Destroy(gameObject);
    }
}
