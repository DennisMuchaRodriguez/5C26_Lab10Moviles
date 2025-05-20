using Firebase.Database;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIMainMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_Text username;
    [SerializeField] private Button playButton;
    [SerializeField] private ScoreDataSO userData;
    [SerializeField] private DatabaseHandler dbHandler;

    void Start()
    {
        username.text = "Write your Username";
        playButton.gameObject.SetActive(false);
    }

    public void OnConfirmUsername()
    {
        string inputUsername = usernameInput.text.Trim();
        if (string.IsNullOrEmpty(inputUsername)) return;

        usernameInput.interactable = false;

        StartCoroutine(CreateUserWithValidation(inputUsername));
    }

    private IEnumerator CreateUserWithValidation(string username)
    {
        bool userExists = false;
        string existingUserId = "";

        var checkQuery = dbHandler.DatabaseReference.Child("users")
            .OrderByChild("normalizedUsername")
            .EqualTo(username.ToLower())
            .LimitToFirst(1);

        var checkTask = checkQuery.GetValueAsync();

        yield return new WaitUntil(() => checkTask.IsCompleted);

        if (checkTask.Result != null && checkTask.Result.ChildrenCount > 0)
        {
            foreach (DataSnapshot child in checkTask.Result.Children)
            {
                userExists = true;
                existingUserId = child.Key;
                break;
            }
        }

        if (userExists)
        {
            userData.userId = existingUserId;
            userData.username = username;
            playButton.gameObject.SetActive(true);
        }
        else
        {
            dbHandler.CreateOrGetUser(username, (userId) => {
                userData.userId = userId;
                userData.username = username;
                playButton.gameObject.SetActive(true);
            });
        }

        usernameInput.interactable = true;

        this.username.text = userData.username;
    }

    public void LoadScene(string name)
    {
        GlobalSceneManager.Instance.LoadNormal(name);
    }

    public void ExitGame()
    {
        GlobalSceneManager.Instance.CloseGame();
    }
}