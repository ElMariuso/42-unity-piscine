using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int damages = 1;
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float hp = 1.0f;
    [SerializeField] private int energyAmount = 1;

    public Rigidbody2D rb { get; private set; }
    public float currentSpeed { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        currentSpeed = moveSpeed;
    }

    private void Update()
    {
        if (GameManager.Instance.isGameOver)
            DestroyEnemy();
    }

    private void FixedUpdate()
    {
        if (rb != null)
        {
            currentSpeed += Time.fixedDeltaTime;
            rb.velocity = new Vector2(0, -currentSpeed);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("MapLimits"))
            DestroyEnemy();
    }

    public void TakeDamage(float amount)
    {
        hp = hp - amount;

        if (hp <= 0)
        {
            GameManager.Instance.AddEnergy(energyAmount);
            Destroy(gameObject);
        }
    }

    public int GetDamages() { return (damages); }
    public void DestroyEnemy() { Destroy(gameObject); }
}
