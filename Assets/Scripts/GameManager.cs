using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // Добавляем для работы со сценами

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;

    public bool isGameOver { get; private set; } = false;

    public static GameManager instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    public GameObject gameOverPanel;

    public float initialEnemySpeed = 3f;
    public float maxEnemySpeed = 100f;
    public float speedIncreaseRate = 0.5f;

    public float initialSpawnInterval = 2f;
    public float minSpawnInterval = 0.7f;
    public float spawnIntervalDecreaseRate = 0.02f;

    public float initialPlayerSpeed = 10f;
    public float maxPlayerSpeed = 100f;
    public float playerSpeedIncreaseRate = 0.1f;

    float timeAlive = 0f;
    private float currentEnemySpeed;
    private float currentSpawnInterval;
    private float currentPlayerSpeed;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Application.targetFrameRate = 60;

        gameOverPanel.SetActive(false);
        highscoreText.text = "Best: " + PlayerPrefs.GetInt("Highscore", 0).ToString();

        currentEnemySpeed = initialEnemySpeed;
        currentSpawnInterval = initialSpawnInterval;
        currentPlayerSpeed = initialPlayerSpeed;
    }

    void Update()
    {
        if (isGameOver) return;

        timeAlive += Time.deltaTime;
        scoreText.text = "Time: " + Mathf.FloorToInt(timeAlive).ToString();

        currentEnemySpeed = Mathf.Min(initialEnemySpeed + timeAlive * speedIncreaseRate, maxEnemySpeed);
        currentSpawnInterval = Mathf.Max(initialSpawnInterval - timeAlive * spawnIntervalDecreaseRate, minSpawnInterval);
        currentPlayerSpeed = Mathf.Min(initialPlayerSpeed + timeAlive * playerSpeedIncreaseRate, maxPlayerSpeed);
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true);

        int finalScore = Mathf.FloorToInt(timeAlive);
        int savedHigh = PlayerPrefs.GetInt("Highscore", 0);

        if (finalScore > savedHigh)
        {
            PlayerPrefs.SetInt("Highscore", finalScore);
            highscoreText.text = "Best: " + finalScore.ToString();
        }

        // Показываем набранный счет на панели с результатами
        finalScoreText.text = "Ваш результат: " + finalScore + " сек.";

        if (FindObjectOfType<EnemySpawner>() != null)
        {
            FindObjectOfType<EnemySpawner>().StopSpawning();
        }
    }


    // --- Новый метод для перезапуска игры ---
    public void RestartGame()
    {
        // Перезагружаем текущую сцену
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public float GetCurrentEnemySpeed()
    {
        return currentEnemySpeed;
    }

    public float GetCurrentSpawnInterval()
    {
        return currentSpawnInterval;
    }

    public float GetCurrentPlayerSpeed()
    {
        return currentPlayerSpeed;
    }
}