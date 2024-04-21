using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform currentTarget;

    // Start is called before the first frame update
    void Start()
    {
        SetTarget(GameObject.FindGameObjectWithTag("Character1").transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsGameOver)
            return ;
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SetTarget(GameObject.FindGameObjectWithTag("Character1").transform);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            SetTarget(GameObject.FindGameObjectWithTag("Character2").transform);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            SetTarget(GameObject.FindGameObjectWithTag("Character3").transform);

        if (currentTarget != null)
        {
            Vector3 newPosition = currentTarget.position;
            newPosition.x = 7;
            newPosition.y += currentTarget.transform.localScale.y * 0.66f;
            transform.position = newPosition;
            transform.LookAt(currentTarget);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        currentTarget = newTarget;
    }
}
