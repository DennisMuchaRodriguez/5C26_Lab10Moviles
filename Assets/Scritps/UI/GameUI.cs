using TMPro;
using UnityEngine;


public class GameUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;

    public void UpdateCurrentScoreDisplay(int score)
    {
        scoreText.text = $"Score: {score}";
    }

    public void UpdateHighScoreDisplay(int highScore)
    {
        highScoreText.text = $"HighScore: {highScore}";
    }
}
