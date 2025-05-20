using Firebase.Database;
using System.Collections;
using UnityEngine;

public class DatabaseHandler : MonoBehaviour
{
    private DatabaseReference _databaseReference;

    public DatabaseReference DatabaseReference
    {
        get
        {
            return _databaseReference;
        }
    }

    void Awake()
    {
        _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SaveScore(ScoreData scoreData)
    {
        CheckUserExists(scoreData.userId, (exists) => {
            if (exists)
            {
                DatabaseReference newEntry = _databaseReference.Child("scores").Push();
                newEntry.SetRawJsonValueAsync(JsonUtility.ToJson(scoreData)); // Ya está correcto
            }
        });
    }

    public void CreateOrGetUser(string username, System.Action<string> callback)
    {
        StartCoroutine(CreateUserCoroutine(username, callback));
    }

    private IEnumerator CreateUserCoroutine(string username, System.Action<string> callback)
    {
        string cleanUsername = username.Trim().ToLower();

        var checkQuery = _databaseReference.Child("users")
            .OrderByChild("normalizedUsername")
            .EqualTo(cleanUsername)
            .LimitToFirst(1);

        var checkTask = checkQuery.GetValueAsync();

        yield return new WaitUntil(() => checkTask.IsCompleted);

        // Corregir la validación de usuarios existentes
        if (checkTask.Result != null && checkTask.Result.ChildrenCount > 0)
        {
            foreach (DataSnapshot child in checkTask.Result.Children)
            {
                // Verificar coincidencia exacta del username normalizado
                if (child.Child("normalizedUsername").Value.ToString() == cleanUsername)
                {
                    callback?.Invoke(child.Key);
                    yield break;
                }
            }
        }

        // Crear nuevo usuario si no existe
        DatabaseReference newUserRef = _databaseReference.Child("users").Push();
        var userData = new UserData
        {
            username = username,
            normalizedUsername = cleanUsername
        };

        var createTask = newUserRef.SetRawJsonValueAsync(JsonUtility.ToJson(userData));
        yield return new WaitUntil(() => createTask.IsCompleted);

        callback?.Invoke(newUserRef.Key);
    }

    private void CheckUserExists(string userId, System.Action<bool> callback)
    {
        _databaseReference.Child("users/" + userId).GetValueAsync().ContinueWith(task => {
            callback?.Invoke(task.Result.Exists);
        });
    }

    public void GetUserHighScore(string username, System.Action<int> callback)
    {
        StartCoroutine(GetHighScoreCoroutine(username, callback));
    }

    private IEnumerator GetHighScoreCoroutine(string username, System.Action<int> callback)
    {
        var query = _databaseReference.Child("scores")
            .OrderByChild("username")
            .EqualTo(username)
            .LimitToLast(1);

        var task = query.GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        int highScore = 0;
        if (task.Result != null && task.Result.Exists)
        {
            foreach (DataSnapshot child in task.Result.Children)
            {
                ScoreData entry = JsonUtility.FromJson<ScoreData>(child.GetRawJsonValue());
                if (entry.score > highScore) highScore = entry.score;
            }
        }

        callback?.Invoke(highScore);
    }

}

[System.Serializable]
public class UserData
{
    public string username;
    public string normalizedUsername;
}