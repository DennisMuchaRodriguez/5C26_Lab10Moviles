using UnityEngine;

[CreateAssetMenu(fileName = "ScoreDataSO", menuName = "Scriptable Objects/ScoreDataSO")]
public class ScoreDataSO : ScriptableObject
{
    public string userId;
    public string username;
    public int currentScore;
    public int highScore;

    public ScoreData ToScoreData()
    {
        return new ScoreData
        {
            userId = this.userId,
            username = this.username,
            score = this.currentScore
        };
    }

    public void ResetData()
    {
        userId = string.Empty;
        username = string.Empty;
        currentScore = 0;
        highScore = 0;
    }
}

[System.Serializable]
public class ScoreData
{
    public string userId;
    public string username;
    public int score;
}