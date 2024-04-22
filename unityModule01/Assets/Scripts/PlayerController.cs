using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Object
    [SerializeField] private Rigidbody rb;
    private static GameObject activeCharacter = null;
    [SerializeField] private GameObject markerPrefab;

    // Movement values
    [SerializeField] private float movespeed = 6.0f;
    [SerializeField] private float jumpForce = 8.0f;

    // Other values
    private float currentHorizontalSpeed;
    private bool isActive = false;
    private bool isGrounded = true;
    private float bufferCheckDistance = 0.1f;
    private float horizontalVelocity = 0.0f;
    private float smoothTime = 0.1f;

    // Start is called before the first frame update
    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    private void Update()
    {
        if (!GameManager.Instance.IsGameOver)
        {
            isGrounded = IsGrounded();
            HandleActivation();
            if (isActive && Input.GetKeyDown(KeyCode.Space) && isGrounded)
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (isActive && !GameManager.Instance.IsGameOver)
            CharacterMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "DeathByHole":
                ApplyGameOverEffects(other);
                break ;
            case "DeathByTurretRed":
                if (gameObject.tag == "Character1")
                {
                    gameObject.SetActive(false);
                    ApplyGameOverEffects(other);
                }
                break ;
            case "DeathByTurretBlue":
                if (gameObject.tag == "Character2")
                {
                    gameObject.SetActive(false);
                    ApplyGameOverEffects(other);
                }
                break ;
            case "DeathByTurretYellow":
                if (gameObject.tag == "Character3")
                {
                    gameObject.SetActive(false);
                    ApplyGameOverEffects(other);
                }
                break ;
            case "DeathByTrapOrTurret":
                gameObject.SetActive(false);
                ApplyGameOverEffects(other);
                break ;
        }
    }

    private void ApplyGameOverEffects(Collider other)
    {
        GameObject marker = GenerateMarkerAtPlayerPosition();
        Camera.main.GetComponent<CameraController>().SetTarget(marker.transform);
        Debug.Log("Game Over");
        GameManager.Instance.GameOver();
    }

    private GameObject GenerateMarkerAtPlayerPosition()
    {
        Vector3 markerPosition = transform.position;
        GameObject marker = Instantiate(markerPrefab, markerPosition, Quaternion.identity);
        return marker;
    }

    private void HandleActivation()
    {
        if (GameManager.Instance.IsGameOver)
            return ;
        
        if ((gameObject.tag == "Character1" && Input.GetKeyDown(KeyCode.Alpha1)) 
            || (gameObject.tag == "Character2" && Input.GetKeyDown(KeyCode.Alpha2))
            || (gameObject.tag == "Character3" && Input.GetKeyDown(KeyCode.Alpha3)))
        {
            if (activeCharacter != null)
                activeCharacter.GetComponent<PlayerController>().isActive = false;
            activeCharacter = gameObject;
            isActive = true;
        }
    }

    private void CharacterMovement()
    {
        float horInput = Input.GetAxis("Horizontal");
        float targetSpeed = horInput * movespeed;

        currentHorizontalSpeed = Mathf.SmoothDamp(currentHorizontalSpeed, targetSpeed, ref horizontalVelocity, smoothTime);
        transform.Translate(new Vector3(0, 0, currentHorizontalSpeed) * Time.deltaTime, Space.World);
    }

    private bool IsGrounded()
    {
        Vector3 origin = transform.position;
        float distanceToGround = GetComponent<BoxCollider>().size.y / 2 + bufferCheckDistance;
        float depth = GetComponent<BoxCollider>().size.z / 2;

        Vector3[] raycastOrigins = {
            origin,
            origin + Vector3.forward * depth,
            origin - Vector3.forward * depth
        };

        foreach (var rayOrigin in raycastOrigins)
        {
            Debug.DrawRay(rayOrigin, Vector3.down * distanceToGround, Color.red);
            if (Physics.Raycast(rayOrigin, Vector3.down, distanceToGround))
                return true;
        }
        return false;
    }
}
