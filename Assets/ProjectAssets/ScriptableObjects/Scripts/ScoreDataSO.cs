using Firebase.Auth;
using Firebase.Firestore;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreDataSO", menuName = "Scriptable Objects/ScoreDataSO")]
public class ScoreDataSO : ScriptableObject
{
    public string userId;
    public string email;  
    public int currentScore;
    public int highScore;

    public ScoreData ToScoreData()
    {
        return new ScoreData
        {
            userId = this.userId,
            email = this.email,  
            score = this.currentScore,
            timestamp = Timestamp.GetCurrentTimestamp()
        };
    }

    public void UpdateFromAuth(FirebaseUser user)
    {
        userId = user.UserId;
        email = user.Email;  
    }

    public void ResetData()
    {
        userId = string.Empty;
        email = string.Empty;  
        currentScore = 0;
        highScore = 0;
    }
}
[FirestoreData]
[System.Serializable]
public struct ScoreData
{
    [FirestoreProperty] public string userId { get; set; }
    [FirestoreProperty] public string email { get; set; }  
    [FirestoreProperty] public int score { get; set; }
    [FirestoreProperty] public Timestamp timestamp { get; set; }
}