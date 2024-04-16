using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 6.0f;
    public Transform cam;
    public float turnSpeed = 720.0f;
    public float jumpForce = 6.0f;
    public LayerMask groundLayer;
    public float raycastDistance = 0.6f;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckGroundStatus();
        ProcessJumpInput();
    }

    private void FixedUpdate()
    {
        ProcessMovement();
        ProcessRotation();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            cam.parent = null;
            Destroy(gameObject);
            Debug.Log("Game Over");
        }
    }

    private void CheckGroundStatus()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, raycastDistance, groundLayer);
    }

    private void ProcessJumpInput()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void ProcessMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = (cam.transform.forward * vertical + cam.transform.right * horizontal).normalized;
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void ProcessRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * turnSpeed * Time.fixedDeltaTime;
        Quaternion turnOffset = Quaternion.Euler(0, mouseX, 0);
        rb.MoveRotation(rb.rotation * turnOffset);
    }
}