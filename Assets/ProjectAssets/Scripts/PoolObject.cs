using UnityEngine;

[CreateAssetMenu(fileName = "PoolObject", menuName = "Pooling/Pool Object")]
public class PoolObject : ScriptableObject
{
    public GameObject prefab;
}