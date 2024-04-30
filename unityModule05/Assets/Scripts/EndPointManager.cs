using UnityEngine;

public class EndPointManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            GameManager.Instance.FinishLevel();
    }
}
