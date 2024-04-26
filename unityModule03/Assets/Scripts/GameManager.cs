using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool isGameOver { get; private set; }

    // Game Attributes
    [SerializeField] private int maxEnergy = 10;
    [SerializeField] private int energy = 10;
    private BarScript energyBar;
    private Spawner spawner;
    private Base base1;
    private int activeEnemies = 0;
    private int totalEnemiesSpawned = 0;
    private int numberOfSpawn = 0;

    // Manage scenes
    public int currentLevel = 1;
    public int maxLevel = 2;

    public float score;

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

    private void Start()
    {
        NoGameOver();
    }

    public void SetupSpawner(Spawner sp)
    {
        spawner = sp;
        if (spawner != null)
        {
            numberOfSpawn = spawner.numberOfSpawn;
        }
    }

    public void ResetValues()
    {
        NoGameOver();
        activeEnemies = 0;
        totalEnemiesSpawned = 0;
        Time.timeScale = 1f;
        score = 0;
        energy = maxEnergy;
    }

    public void SetupBase(Base base2)
    {
        base1 = base2;
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
        score = CalculateScore();
        SceneManager.LoadScene("Score");
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
            score = CalculateScore();
            SceneManager.LoadScene("Score");
        }
    }

    public float CalculateScore()
    {
        float healthScore = (float)base1.hp / base1.maxHp * 100;
        float energyScore = (float)energy / maxEnergy * 100;
        return ((healthScore + energyScore) / 2);
    }

    public void LoadNextLevel()
    {
        currentLevel++;
        if (currentLevel <= maxLevel)
            SceneManager.LoadScene("Map" + currentLevel);
        else
            SceneManager.LoadScene("EndGame");
    }
}
