using UnityEngine;

public class GameOverDataHandler : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private ScoreDataSO scoreData;
    [SerializeField] private DatabaseHandler databaseHandler;

    [Header("Settings")]
    [SerializeField] private float scoreSaveDelay = 0.5f;

    private bool newHighscore = false;

    private void Start()
    {
        SaveAndValidateScore();
    }

    private void SaveAndValidateScore()
    {
        databaseHandler.SaveScore(scoreData.ToScoreData());

        if (scoreData.currentScore > scoreData.highScore)
        {
            scoreData.highScore = scoreData.currentScore;
            newHighscore = true;
        }

        databaseHandler.GetUserHighScore(scoreData.username, (highScore) => {
            scoreData.highScore = Mathf.Max(highScore, scoreData.highScore);
        });
    }

    public bool IsNewHighscore()
    {
        return newHighscore;
    }
}