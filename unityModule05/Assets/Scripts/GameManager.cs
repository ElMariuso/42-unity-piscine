using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    /* Start Point */
    [SerializeField] private GameObject startPoint;
    public Vector3 startPosition;

    /* Score */
    private int totalScore = 0;
    private int scoreToFinishLevel = 25;
    private int score = 0;

    /* Level Booleans */
    private bool canExit = false;

    /* Start Functions */
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        startPosition = startPoint.transform.position;
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
        if (!canExit) return ;

        totalScore += score;
        Debug.Log("Finish level");
    }
}
