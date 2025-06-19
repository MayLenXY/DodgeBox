using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainMenuPanel;       // ������ � ������� "�����"
    public GameObject leaderboardPanel;    // ������ � �������� �������

    [Header("Leaderboard Texts")]
    public TextMeshProUGUI[] scoreTexts;   // ������ ��� ����������� ���-5

    // ������ ����
    public void OnPlay()
    {
        SceneManager.LoadScene("GameScene");
    }

    // ����� ������� �������
    public void OnShowLeaderboard()
    {
        mainMenuPanel.SetActive(false);        // �������� ��������� ����
        leaderboardPanel.SetActive(true);      // ���������� ������� �������
        LoadLeaderboard();
    }

    // �������� ������� �������
    public void OnHideLeaderboard()
    {
        leaderboardPanel.SetActive(false);     // �������� �������
        mainMenuPanel.SetActive(true);         // ���������� ���� �������
    }

    // �������� �������� �� PlayerPrefs
    private void LoadLeaderboard()
    {
        for (int i = 0; i < scoreTexts.Length; i++)
        {
            int score = PlayerPrefs.GetInt("Score" + i, 0);
            scoreTexts[i].text = $"{i + 1}. {score} ���.";
        }
    }
}
