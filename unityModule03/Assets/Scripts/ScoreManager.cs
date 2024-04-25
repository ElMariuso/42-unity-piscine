using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text mainText;   
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text rankText;
    private float score = 0.0f;
    private bool isGameOver = false;

    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            score = GameManager.Instance.score;
            isGameOver = GameManager.Instance.isGameOver;
            InitMainText();
            InitScoreText();
            InitRankText();
        }
    }

    void InitMainText()
    {
        if (isGameOver)
            mainText.text = "GAME OVER";
        else
            mainText.text = "MAP-" + GameManager.Instance.currentLevel + " COMPLETED";
    }

    void InitScoreText()
    {
        scoreText.text = $"{score}";
    }

    void InitRankText()
    {
        switch (score)
        {
            case float n when (score >= 0 && score < 20):
                rankText.text = "Rookie Runner (F)";
                break ;
            case float n when (score >= 20 && score < 40):
                rankText.text = "Battle Bruised (D)";
                break ;
            case float n when (score >= 40 && score < 60):
                rankText.text = "Adept Adventurer (C)";
                break ;
            case float n when (score >= 60 && score < 80):
                rankText.text = "Skillful Survivor (B)";
                break ;
            case float n when (score >= 80 && score < 100):
                rankText.text = "Elite Enforcer (A)";
                break ;
            case float n when (score == 100.0f):
                rankText.text = "Supreme Sentinel (S)";
                break ;
        }
    }
}
