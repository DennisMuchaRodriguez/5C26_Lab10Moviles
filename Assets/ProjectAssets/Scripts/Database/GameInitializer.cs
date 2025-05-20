using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private ScoreDataSO scoreData;

    void Awake()
    {
        scoreData.ResetData();
    }
}