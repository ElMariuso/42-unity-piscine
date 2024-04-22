using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool isGameOver { get; private set; }

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
        NoGameOver();
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        isGameOver = true;
    }

    public void NoGameOver()
    {
        isGameOver = false;
    }
}
