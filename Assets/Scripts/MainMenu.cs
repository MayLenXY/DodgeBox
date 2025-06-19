using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainMenuPanel;       // Панель с кнопкой "Старт"
    public GameObject leaderboardPanel;    // Панель с таблицей лидеров

    [Header("Leaderboard Texts")]
    public TextMeshProUGUI[] scoreTexts;   // Тексты для отображения топ-5

    // Запуск игры
    public void OnPlay()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Показ таблицы лидеров
    public void OnShowLeaderboard()
    {
        mainMenuPanel.SetActive(false);        // Скрываем стартовое меню
        leaderboardPanel.SetActive(true);      // Показываем таблицу лидеров
        LoadLeaderboard();
    }

    // Закрытие таблицы лидеров
    public void OnHideLeaderboard()
    {
        leaderboardPanel.SetActive(false);     // Скрываем таблицу
        mainMenuPanel.SetActive(true);         // Показываем меню обратно
    }

    // Загрузка значений из PlayerPrefs
    private void LoadLeaderboard()
    {
        for (int i = 0; i < scoreTexts.Length; i++)
        {
            int score = PlayerPrefs.GetInt("Score" + i, 0);
            scoreTexts[i].text = $"{i + 1}. {score} сек.";
        }
    }
}
