using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public AudioSource respawnSound;
    [SerializeField] Animator animator;
    [SerializeField] Animator playerAnimator;
    [SerializeField] GameObject player;
    [SerializeField] Vector3 startPosition;

    public void OnFadeComplete()
    {
        StartCoroutine(ResetPlayerAfterDelay());
    }

    private IEnumerator ResetPlayerAfterDelay()
    {
        yield return new WaitForSeconds(1);
        player.SetActive(true);
        ResetPlayerPosition();
    }

    private void ResetPlayerPosition()
    {
        player.transform.position = startPosition;
        playerAnimator.SetBool("IsRespawning", true);
        animator.SetTrigger("FadeToWhite");
        respawnSound.Play();
    }

    private void Reset()
    {
        animator.SetTrigger("FadeToClear");
    }
}
