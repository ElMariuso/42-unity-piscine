using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int damages = 1;
    [SerializeField] private float moveSpeed = 1.0f;

    private Rigidbody2D rb;
    private float currentSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        currentSpeed = moveSpeed;
    }

    private void FixedUpdate()
    {
        currentSpeed += Time.fixedDeltaTime;
        rb.velocity = new Vector2(0, -currentSpeed);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "MapLimits")
            DestroyEnemy();
    }

    public int GetDamages() { return (damages); }
    public void DestroyEnemy() { Destroy(gameObject); }
}
