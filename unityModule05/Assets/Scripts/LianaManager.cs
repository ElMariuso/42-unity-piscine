using UnityEngine;

public class LianaManager : MonoBehaviour
{
    public AudioSource attackSound;
    [SerializeField] private GameObject target;
    [SerializeField] private Animator animator;
    [SerializeField] private int damage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            animator.SetBool("IsAttacking", true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            animator.SetBool("IsAttacking", false);
    }

    public void MakeDamages()
    {
        if (target != null)
        {
            PlayerController player = target.GetComponent<PlayerController>();

            attackSound.Play();
            player.TakeDamages(damage);
        }
    }
}
