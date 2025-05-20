using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RankingManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private int defaultEntries = 5;
    [SerializeField] private UIRankingManager uiRankingManager;

    private DatabaseReference _scoresReference;

    void Start()
    {
        _scoresReference = FirebaseDatabase.DefaultInstance.GetReference("scores");
        LoadTopScores(defaultEntries);
    }

    public void LoadTopScores(int numberOfEntries)
    {
        StartCoroutine(LoadTopScoresCoroutine(numberOfEntries));
    }

    private IEnumerator LoadTopScoresCoroutine(int maxEntries)
    {
        var query = _scoresReference
            .OrderByChild("score")
            .LimitToLast(maxEntries);

        var task = query.GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task != null && task.IsCompleted && !task.IsFaulted)
            ProcessSnapshot(task.Result);
        else
            Debug.LogError("Error loading scores");
    }

    private void ProcessSnapshot(DataSnapshot snapshot)
    {
        List<ScoreData> scores = new List<ScoreData>();

        foreach (DataSnapshot child in snapshot.Children)
        {
            try
            {
                ScoreData entry = JsonUtility.FromJson<ScoreData>(child.GetRawJsonValue());
                scores.Add(entry);
            }
            catch (System.ArgumentException e)
            {
                Debug.LogError($"Error parsing JSON: {child.Key} - {e.Message}");
            }
        }

        scores.Sort((a, b) => b.score.CompareTo(a.score));

        uiRankingManager.UpdateRankingUI(scores);
    }
}