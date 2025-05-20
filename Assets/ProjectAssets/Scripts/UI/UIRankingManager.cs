using UnityEngine;
using System.Collections.Generic;

public class UIRankingManager : MonoBehaviour
{
    [SerializeField] private UIRankingEntry[] _rankingEntries;

    public void UpdateRankingUI(List<ScoreData> scores)
    {
        for (int i = 0; i < _rankingEntries.Length; i++)
        {
            if (i < scores.Count)
            {
                _rankingEntries[i].SetData(scores[i].username, scores[i].score);
            }
            else
            {
                _rankingEntries[i].SetData("---", 0);
            }
        }
    }

    public void LoadScene(string name)
    {
        GlobalSceneManager.Instance.LoadNormal(name);
    }
}