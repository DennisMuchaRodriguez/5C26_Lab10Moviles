using TMPro;
using UnityEngine;
using UnityEngine.UI;
/*
public class GameOverUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private GameObject newRecordText;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button menuButton;

    private void Start()
    {
        
        playAgainButton.onClick.AddListener(OnPlayAgainClicked);
        menuButton.onClick.AddListener(OnMenuClicked);

     
        scoreText.text = $"Puntaje: {GameManager.Instance.CurredScore}";
        highScoreText.text = $"Récord: {GameManager.Instance.HighScore}";
        newRecordText.SetActive(GameManager.Instance.IsNewRecord());
    }

    private void OnPlayAgainClicked()
    {
        GameManager.Instance.StartGame();
    }

    private void OnMenuClicked()
    {
        GameManager.Instance.ReturnToMenu();
    }
}
*/