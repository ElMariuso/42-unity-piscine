using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private AudioSource backgroundMusic;

    /* Start Point */
    private GameObject startPoint;
    public Vector3 startPosition;

    /* UI */
    private EndPointManager endPointManager;

    /* Score */
    private int totalScore = 0;
    private int scoreToFinishLevel = 25;
    private int score = 0;

    /* Level Booleans */
    private bool canExit = false;

    /* Start Functions */
    private void Awake()
    {
        // backgroundMusic.Play();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        startPoint = GameObject.FindGameObjectWithTag("StartPoint");
        endPointManager = FindObjectOfType<EndPointManager>();
        if (startPoint != null)
            startPosition = startPoint.transform.position;
        else
            Debug.LogError("StartPoint not found in the scene!");

        if (endPointManager == null)
            Debug.LogError("EndPointManager not found in the scene!");
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /* Update Functions */
    private void Update()
    {

    }

    /* Pause and Resume */
    public void Pause()
    {

    }

    public void Resume()
    {
        
    }

    /* Score and Level Management */
    public void addScore(int point)
    {
        score += point;
        
        if (score >= scoreToFinishLevel)
            canExit = true;
    }

    public void FinishLevel()
    {
        if (!canExit) 
        {
            cantFinishLevel();
            return;
        }

        totalScore += score;
        score = 0;
        canExit = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void cantFinishLevel()
    {
        if (endPointManager != null)
            endPointManager.ShowError();
        else
            Debug.LogError("EndPointManager not set. Cannot show error message.");
    }
}