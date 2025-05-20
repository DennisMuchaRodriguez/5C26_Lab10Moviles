using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolDynamic : MonoBehaviour
{
    [SerializeField] private PoolObject poolConfig;
    [SerializeField] private List<GameObject> objectPool = new List<GameObject>();

    private void Start()
    {
        ValidatePrefab();
    }

    private void ValidatePrefab()
    {
        if (poolConfig != null && poolConfig.prefab.GetComponent<Poolable>() == null)
        {
            Debug.LogError("Prefab no tiene componente Poolable!", poolConfig.prefab);
        }
    }

    public GameObject GetObject(Vector3 position, Quaternion rotation)
    {
        GameObject obj = FindFirstInactiveObject();

        if (obj == null)
        {
            obj = CreateNewPooledObject();
        }

        PrepareObject(obj, position, rotation);
        return obj;
    }

    private GameObject FindFirstInactiveObject()
    {
        foreach (var pooledObj in objectPool)
        {
            if (!pooledObj.activeInHierarchy) return pooledObj;
        }
        return null;
    }

    private GameObject CreateNewPooledObject()
    {
        GameObject obj = Instantiate(poolConfig.prefab, transform);
        obj.SetActive(false);
        InitializePoolableComponent(obj);
        objectPool.Add(obj);
        return obj;
    }

    private void InitializePoolableComponent(GameObject obj)
    {
        Poolable poolable = obj.GetComponent<Poolable>();
        if (poolable != null)
        {
            poolable.InitializeFromPool(this);
        }
        else
        {
            Debug.LogError("Objeto poolable no inicializado", obj);
        }
    }

    private void PrepareObject(GameObject obj, Vector3 position, Quaternion rotation)
    {
        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);
        Poolable poolable = GetComponent<Poolable>();
        if (poolable != null)
        {
            poolable.ResetState();
        }
    }

    public void SetObject(GameObject obj)
    {
        if (objectPool.Contains(obj) == false)
        {
            Debug.LogError("Objeto no pertenece al pool!", obj);
            return;
        }

        obj.SetActive(false);
    }
}