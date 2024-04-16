using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Object
    [SerializeField] private Rigidbody rb;
    private static GameObject activeCharacter = null;

    // Movement values
    [SerializeField] private float movespeed = 3.0f;
    [SerializeField] private float jumpForce = 8.0f;

    // Other values
    private bool isActive = false;
    private bool isGrounded = true;

    // Start is called before the first frame update
    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    private void Update()
    {
        HandleActivation();
        if (isActive && Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (isActive)
            CharacterMovement();
    }

    private void HandleActivation()
    {
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
        Vector3 movement = new Vector3(0, 0, horInput).normalized;

        transform.Translate(movement * movespeed * Time.deltaTime);
    }
}
