using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private int hp = 5;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                TakeDamage(enemy.GetDamages());
                enemy.DestroyEnemy();
            }
        }
    }

    private void TakeDamage(int amount)
    {
        hp = hp - amount;

        if (hp <= 0)
        {
            GameManager.Instance.GameOver();
            Destroy(gameObject);
        }
        else
            Debug.Log("Your base have now " + hp + " hp!");
    }
}
