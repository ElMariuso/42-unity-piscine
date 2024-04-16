using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Object
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Character character;

    // Movement values
    [SerializeField] private float movespeed = 3.0f;
    [SerializeField] private float jumpForce = 8.0f;

    // Booleans
    private bool isGrounded = true;

    // Start is called before the first frame update
    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    private void Update()
    {
        if (character.isActive && Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (character.isActive)
            CharacterMovement();
    }

    private void CharacterMovement()
    {
        float horInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(0, 0, horInput).normalized;

        transform.Translate(movement * movespeed * Time.deltaTime);
    }
}
