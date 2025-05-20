using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ScoreEntry
{
    public string playerName;
    public int score;
    public string timestamp;
}

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

          
            Debug.Log("Firebase Manager inicializado");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveScore(string playerName, int score, bool isNewRecord)
    {
        
        Debug.Log($"Puntaje guardado: {playerName} - {score} ({(isNewRecord ? "Nuevo récord" : "No récord")})");

        // Aquí se implementa la conexión real a Firebase villabot, esto es como una simulacion
    }

    public void GetTopScores(int count, Action<List<ScoreEntry>> callback)
    {
        // Simulando puntajes
        var sampleScores = new List<ScoreEntry>
        {
            new ScoreEntry { playerName = "Jugador1", score = 4500 },
            new ScoreEntry { playerName = "Jugador2", score = 3800 },
            new ScoreEntry { playerName = "Jugador3", score = 3200 },
            new ScoreEntry { playerName = "Jugador4", score = 2900 },
            new ScoreEntry { playerName = "Jugador5", score = 2500 }
        };

        callback?.Invoke(sampleScores);
    }
}