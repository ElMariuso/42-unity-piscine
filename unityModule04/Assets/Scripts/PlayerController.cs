using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed = 40f;
    [SerializeField] private float jumpForce = 400f;
    private float horizontalMove = 0f;
    private bool jump = false;
    private bool facingRight = true;

    private Vector2 velocity = Vector2.zero;

    private Rigidbody2D rb;
    private bool isGrounded = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;

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
        else if (rb.velocity.y <= 0)
            animator.SetBool("IsFalling", true);
    }

    void FixedUpdate()
    {
        Vector2 targetVelocity = new Vector2(horizontalMove * Time.fixedDeltaTime, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);

        if (jump)
        {
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
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsFalling", false);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
