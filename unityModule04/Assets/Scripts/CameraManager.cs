using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset;

    void LateUpdate()
    {
        if (target == null) return;
        
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
