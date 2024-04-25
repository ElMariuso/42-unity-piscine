using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public int maxHp = 5;
    public int hp = 5;
    [SerializeField] private BarScript hpBar;

    private void Awake()
    {
        if (hpBar != null)
            hpBar.SetMaxValue(maxHp);
        hpBar.SetValue(hp);

        Base bs = GetComponent<Base>();
        if (GameManager.Instance != null)
            GameManager.Instance.SetupBase(bs);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                TakeDamage(enemy.GetDamages());
                GameManager.Instance.EnemyDestroyed();
                enemy.DestroyEnemy();
            }
        }
    }

    private void TakeDamage(int amount)
    {
        hp = hp - amount;

        if (hpBar != null)
            hpBar.SetValue(hp);

        if (hp <= 0)
        {
            GameManager.Instance.GameOver();
            Destroy(gameObject);
        }
    }
}
