using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool isGameOver { get; private set; }

    // Game Attributes
    [SerializeField] private int maxEnergy = 10;
    [SerializeField] private int energy = 10;
    private BarScript energyBar;
    private int activeEnemies = 0;
    private int totalEnemiesSpawned = 0;

    private void Awake()
    {
        Time.timeScale = 1f;
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

    private void Start()
    {
        NoGameOver();
    }

    public void SetupEnergyBar(BarScript bar)
    {
        energyBar = bar;
        if (energyBar != null)
        {
            energyBar.SetMaxValue(maxEnergy);
            energyBar.SetValue(energy);
        }
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

    public void AddEnergy(int amount)
    {
        if (energy < maxEnergy)
        {
            energy += amount;
            if (energy > maxEnergy)
                energy = maxEnergy;
            if (energyBar != null)
                energyBar.SetValue(energy);
        }
    }

    public void RemoveEnergy(int amount)
    {
        energy -= amount;
        if (energyBar != null)
            energyBar.SetValue(energy);
    }

    public bool HasEnoughEnergy(int amount)
    {
        return (energy - amount >= 0);
    }

    public void EnemySpawned()
    {
        activeEnemies++;
        totalEnemiesSpawned++;
    }

    public void EnemyDestroyed()
    {
        activeEnemies--;
        CheckEndOfLevel();
    }

    private void CheckEndOfLevel()
    {
        if (activeEnemies <= 0 && totalEnemiesSpawned >= numberOfSpawn)
        {
            Debug.Log("Score " + CalculateScore());
        }
    }

    public float CalculateScore()
    {
        float healthScore = (float)Base.Instance.hp / Base.Instance.maxHp * 100;
        float energyScore = (float)energy / maxEnergy * 100;
        return ((healthScore + energyScore) / 2);
    }
}
