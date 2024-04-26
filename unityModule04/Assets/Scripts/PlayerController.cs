using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Animator fadeAnimator;
    [SerializeField] private float moveSpeed = 40f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int maxHp = 3;
    [SerializeField] private int hp = 3;

    // Sounds
    public AudioSource jumpSound;
    public AudioSource hitSound;
    public AudioSource deathSound;

    public LayerMask whatIsWall;

    private float horizontalMove = 0f;
    private bool jump = false;
    private bool facingRight = true;

    private Vector2 velocity = Vector2.zero;

    private Rigidbody2D rb;
    private bool isGrounded = true;
    private bool isDeath = false;
    private bool isOnAWall = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isDeath) return ;

        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
        if (isOnAWall && !isGrounded)
        {
            if ((facingRight && horizontalMove > 0) || (!facingRight && horizontalMove < 0))
                horizontalMove = 0;
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }
        if (horizontalMove > 0 && !facingRight)
            Flip();
        else if (horizontalMove < 0 && facingRight)
            Flip();

        if (!isGrounded && rb.velocity.y > 0)
            animator.SetBool("IsFalling", false);
        else if (!isGrounded && rb.velocity.y <= 0)
            animator.SetBool("IsFalling", true);
    }

    private void FixedUpdate()
    {
        if (isDeath) return;

        Vector2 targetVelocity = new Vector2(horizontalMove * Time.fixedDeltaTime, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);

        if (jump)
        {
            jumpSound.Play();
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
            jump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            OnLanding();
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            horizontalMove = 0;
            isOnAWall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            isOnAWall = false;
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsFalling", false);
    }

    public void TakeDamages(int amount)
    {
        hp = hp - amount;

        if (!isDeath)
            hitSound.Play();
        if (hp <= 0 && !isDeath)
        {
            animator.SetBool("IsDeath", true);
            isDeath = true;
            deathSound.Play();
        }
        else if (!isDeath)
        {
            animator.SetBool("IsTakingDamage", true);
        }
    }

    public void NoMoreDamages()
    {
        animator.SetBool("IsTakingDamage", false);
    }

    public void DeathDestroy()
    {
        fadeAnimator.SetTrigger("StartFade");
        DisableGameObject();
    }

    void DisableGameObject()
    {
        gameObject.SetActive(false);
    }

    public void noMoreDeath()
    {
        isDeath = false;
        animator.SetBool("IsRespawning", false);
        hp = maxHp;
        if (!facingRight)
            Flip();
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
