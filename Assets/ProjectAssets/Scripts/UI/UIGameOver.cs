using TMPro;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text currentScoreText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private GameObject newHighscoreBadge;

    [Header("Dependencies")]
    [SerializeField] private GameOverDataHandler dataHandler;
    [SerializeField] private ScoreDataSO scoreData;

    private void Start()
    {
        UpdateUI();
        CheckNewHighscore();
    }

    private void UpdateUI()
    {
        currentScoreText.text = $"Score: {scoreData.currentScore}";
        highScoreText.text = $"HighScore: {scoreData.highScore}";
    }

    private void CheckNewHighscore()
    {
        newHighscoreBadge.SetActive(dataHandler.IsNewHighscore());
    }

    public void LoadScene(string sceneName)
    {
        GlobalSceneManager.Instance.LoadNormal(sceneName);

        scoreData.currentScore = 0;
    }
}