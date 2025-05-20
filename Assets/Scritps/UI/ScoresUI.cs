using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/*
public class ScoresUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private RectTransform scoresContainer;
    [SerializeField] private GameObject scoreEntryPrefab;
    [SerializeField] private Button backButton;
    [SerializeField] private TMP_Text loadingText;

    private void Start()
    {
        // Configurar botón de regreso
        backButton.onClick.AddListener(() => GameManager.Instance.ReturnToMenu());

        // Cargar y mostrar puntuaciones
        LoadLocalScores();
    }

    private void LoadLocalScores()
    {
        // Limpiar contenedor
        foreach (Transform child in scoresContainer)
        {
            Destroy(child.gameObject);
        }

        // Mostrar puntuación actual del jugador
        CreateScoreEntry(1, GameManager.Instance.PlayerName, GameManager.Instance.CurredScore);

        // Mostrar récord personal
        CreateScoreEntry(2, GameManager.Instance.PlayerName + " (Récord)", GameManager.Instance.HighScore);
    }

    private void CreateScoreEntry(int position, string playerName, int score)
    {
        if (scoreEntryPrefab == null || scoresContainer == null) return;

        var entry = Instantiate(scoreEntryPrefab, scoresContainer);
        var texts = entry.GetComponentsInChildren<TMP_Text>();

        if (texts.Length >= 2)
        {
            texts[0].text = $"{position}. {playerName}";
            texts[1].text = score.ToString();
        }
    }
}
*/