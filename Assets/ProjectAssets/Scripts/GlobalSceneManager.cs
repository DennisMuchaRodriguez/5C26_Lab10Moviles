using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GlobalSceneManager : SingletonPersistent<GlobalSceneManager>
{
    private string currentActiveScene;

    private void Start()
    {
        currentActiveScene = SceneManager.GetActiveScene().name;
    }

    public void LoadNormal(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        currentActiveScene = sceneName;
    }

    public void LoadAdditive(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void LoadAsyncNormal(string sceneName)
    {
        StartCoroutine(LoadAsyncNormalRoutine(sceneName));
    }

    public void LoadAsyncAdditive(string sceneName)
    {
        StartCoroutine(LoadAsyncAdditiveRoutine(sceneName));
    }

    private IEnumerator LoadAsyncNormalRoutine(string sceneName)
    {
        string previousScene = currentActiveScene;

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        loadOperation.allowSceneActivation = false;

        while (!loadOperation.isDone)
        {
            if (loadOperation.progress >= 0.9f)
            {
                loadOperation.allowSceneActivation = true;
            }
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        currentActiveScene = sceneName;

        yield return SceneManager.UnloadSceneAsync(previousScene);
    }

    private IEnumerator LoadAsyncAdditiveRoutine(string sceneName)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        yield return loadOperation;
    }

    public void UnloadScene(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName).IsValid())
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }

    public void RemoveScene(string sceneName)
    {
        Scene sceneToRemove = SceneManager.GetSceneByName(sceneName);
        if (sceneToRemove.IsValid())
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}