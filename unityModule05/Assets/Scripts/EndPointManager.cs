using UnityEngine;

public class EndPointManager : MonoBehaviour
{
    [SerializeField] Animator errorAnimator;
    [SerializeField] GameObject errorMessage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            GameManager.Instance.FinishLevel();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            HideError();
    }

    public void ShowError()
    {
        errorMessage.SetActive(true);
        errorAnimator.SetTrigger("FadeIn");
    }

    public void HideError()
    {
        errorAnimator.SetTrigger("FadeOut");
    }

    public void UnactiveErrorMessage()
    {
        errorMessage.SetActive(false);
    }
}
