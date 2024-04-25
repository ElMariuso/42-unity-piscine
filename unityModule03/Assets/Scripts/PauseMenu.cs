using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject mainPauseMenuUI;
    public GameObject confirmationMenuUI;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void OpenConfirmationMenu()
    {
        confirmationMenuUI.SetActive(true);
        mainPauseMenuUI.SetActive(false);
    }

    public void CancelQuit()
    {
        confirmationMenuUI.SetActive(false);
        mainPauseMenuUI.SetActive(true);
    }
}
