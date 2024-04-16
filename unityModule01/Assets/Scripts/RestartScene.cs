using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    // Update is called once per frame
    public void Update()
    {
        Debug.Log("AAAAH");
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log("Restart command received");
            RestartSceneFunction();
        }
    }

    public void RestartSceneFunction()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
