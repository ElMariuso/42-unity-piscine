using UnityEngine;

public class TeleporterManager : MonoBehaviour
{
    [SerializeField] private TeleporterManager exitTeleporter;
    [SerializeField] private float cooldown = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Character"))
        {
            other.transform.position = new Vector3(exitTeleporter.transform.position.x, exitTeleporter.transform.position.y + 1, exitTeleporter.transform.position.z);
            exitTeleporter.DisableTeleporter();
        }
    }

    public void DisableTeleporter()
    {
        GetComponent<Collider>().enabled = false;
        Invoke("EnableTeleporter", cooldown);
    }

    private void EnableTeleporter()
    {
        GetComponent<Collider>().enabled = true;
    }
}
