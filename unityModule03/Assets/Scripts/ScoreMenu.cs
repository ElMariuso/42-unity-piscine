using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreMenu : MonoBehaviour
{
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject replayButton;

    private void Awake()
    {
        if (GameManager.Instance.isGameOver)
        {
            nextButton.SetActive(false);
            replayButton.SetActive(true);
        }
    }

    public void NextStage()
    {
        GameManager.Instance.ResetValues();
        GameManager.Instance.LoadNextLevel();
    }

    public void ReplayStage()
    {
        GameManager.Instance.ResetValues();
        SceneManager.LoadScene("Map" + GameManager.Instance.currentLevel);
    }
}
