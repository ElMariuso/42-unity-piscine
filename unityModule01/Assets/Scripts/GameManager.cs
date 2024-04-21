using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string[] stages;
    private int currentStageIndex = 0;

    public static GameManager Instance { get; private set; }
    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadNextStage()
    {
        currentStageIndex++;
        if (currentStageIndex >= stages.Length)
        {
            currentStageIndex = 0;
        }
        SceneManager.LoadScene(stages[currentStageIndex]);
    }

    public void StartFirstStage()
    {
        SceneManager.LoadScene(stages[0]);
    }

    public void GameOver()
    {
        IsGameOver = true;
    }

    public void NoGameOver()
    {
        IsGameOver = false;
    }
}
