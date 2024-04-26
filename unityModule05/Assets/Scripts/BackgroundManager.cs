using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    private Vector3 startPosition;
    private float startY;

    void Start()
    {
        startPosition = transform.position;
        startY = transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(cameraTransform.position.x, startY, startPosition.z);
    }
}

