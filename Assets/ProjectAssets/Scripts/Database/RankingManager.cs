using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private int maxEntries = 10;
    [SerializeField] private ScoreDataSO currentPlayerData;
    [SerializeField] private UIRankingManager uiManager;

    private FirebaseFirestore db;
    private CollectionReference scoresRef;

    void Start()
    {
        InitializeFirestore();
        LoadTopScores();
    }

    private void InitializeFirestore()
    {
        db = FirebaseFirestore.DefaultInstance;
        scoresRef = db.Collection("scores");
    }

    public void SubmitCurrentScore()
    {
        if (currentPlayerData.currentScore > 0)
        {
            StartCoroutine(SubmitScoreCoroutine());
        }
    }

    private IEnumerator SubmitScoreCoroutine()
    {
        var scoreData = currentPlayerData.ToScoreData();

        var task = scoresRef.AddAsync(scoreData);
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsCompleted && !task.IsFaulted)
        {
            Debug.Log("Score submitted successfully!");

     
            if (currentPlayerData.currentScore > currentPlayerData.highScore)
            {
                currentPlayerData.highScore = currentPlayerData.currentScore;
            }

            LoadTopScores(); 
        }
        else
        {
            Debug.LogError("Failed to submit score: " + task.Exception);
        }
    }

    public void LoadTopScores()
    {
        StartCoroutine(LoadTopScoresCoroutine());
    }

    private IEnumerator LoadTopScoresCoroutine()
    {
        var query = scoresRef
            .OrderByDescending("score")
            .Limit(maxEntries);

        var task = query.GetSnapshotAsync();
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsCompleted && !task.IsFaulted)
        {
            ProcessSnapshot(task.Result);
        }
        else
        {
            Debug.LogError("Error loading scores: " + task.Exception);
        }
    }

    private void ProcessSnapshot(QuerySnapshot snapshot)
    {
        List<ScoreData> scores = new List<ScoreData>();

        foreach (var document in snapshot.Documents)
        {
            var scoreData = document.ConvertTo<ScoreData>();
            scores.Add(scoreData);
        }

        uiManager.UpdateRankingUI(scores);
    }

    public void LoadPlayerScores()
    {
        StartCoroutine(LoadPlayerScoresCoroutine());
    }

    private IEnumerator LoadPlayerScoresCoroutine()
    {
        var query = scoresRef
            .WhereEqualTo("userId", currentPlayerData.userId)
            .OrderByDescending("score")
            .Limit(5);

        var task = query.GetSnapshotAsync();
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsCompleted && !task.IsFaulted)
        {
            List<ScoreData> playerScores = new List<ScoreData>();

            foreach (var document in task.Result.Documents)
            {
                playerScores.Add(document.ConvertTo<ScoreData>());
            }
        }
    }
}