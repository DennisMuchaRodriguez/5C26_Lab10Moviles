using UnityEngine;

public abstract class Poolable : MonoBehaviour
{
    protected ObjectPoolDynamic originPool;

    public virtual void InitializeFromPool(ObjectPoolDynamic pool)
    {
        originPool = pool;
    }

    public virtual void ResetState()
    {
        // Implementar el reinicio de variables 
    }

    public virtual void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}