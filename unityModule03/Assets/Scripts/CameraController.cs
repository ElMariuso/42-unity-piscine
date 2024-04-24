using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 5.0f; // Vitesse de déplacement de la caméra
    private Vector2 minLimits;
    private Vector2 maxLimits;
    private float halfHeight;
    private float halfWidth;

    void Start()
    {
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;

        GameObject mapLimits = GameObject.FindGameObjectWithTag("MapLimits");
        if (mapLimits != null)
        {
            BoxCollider2D limits = mapLimits.GetComponent<BoxCollider2D>();
            if (limits != null)
            {
                minLimits = limits.bounds.min;
                maxLimits = limits.bounds.max;
            }
        }
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);

        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.up * verticalInput * speed * Time.deltaTime);

        float clampedX = Mathf.Clamp(transform.position.x, minLimits.x + halfWidth, maxLimits.x - halfWidth);
        float clampedY = Mathf.Clamp(transform.position.y, minLimits.y + halfHeight, maxLimits.y - halfHeight);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
