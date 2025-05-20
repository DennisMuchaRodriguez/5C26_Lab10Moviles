using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private ScoreDataSO scoreData;
    [SerializeField] private DatabaseHandler databaseHandler;
    [SerializeField] private GameUI gameUI;

    private void Start()
    {
        LoadInitialHighScore();
    }

    private void LoadInitialHighScore()
    {
        databaseHandler.GetUserHighScore(scoreData.username, (highScore) => {
            scoreData.highScore = highScore;
            gameUI.UpdateHighScoreDisplay(scoreData.highScore);
        });
    }

    public void AddScore(int points)
    {
        scoreData.currentScore += points;

        if (scoreData.currentScore > scoreData.highScore)
        {
            scoreData.highScore = scoreData.currentScore;
            gameUI.UpdateHighScoreDisplay(scoreData.highScore);
        }

        gameUI.UpdateCurrentScoreDisplay(scoreData.currentScore);
    }

    public void ResetCurrentScore()
    {
        scoreData.currentScore = 0;
        gameUI.UpdateCurrentScoreDisplay(0);
    }
}