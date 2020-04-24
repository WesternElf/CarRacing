using System.Collections.Generic;
using SpawnFeatures;
using UnityEngine;

public class ObjectPooler : SpawnPoint
{
    private static ObjectPooler _instance;
    public static ObjectPooler Instance => _instance;
    public List<GameObject> pooledOblects;
    public GameObject[] objectToPool;
    public int amountToPool;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        pooledOblects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject) Instantiate(objectToPool[Random.Range(0, objectToPool.Length)], GetPositionForNewObject(), SpawnStartPoint.rotation, EmptyObject.transform);
            obj.SetActive(false);
            pooledOblects.Add(obj);
        }
        spawningCoroutine = StartCoroutine(StartSpawn());
    }

    public GameObject GetPoolObject()
    {
        for (int i = 0; i < pooledOblects.Count; i++)
        {
            if (!pooledOblects[i].activeInHierarchy)
            { 
                return pooledOblects[Random.Range(0, pooledOblects.Count)];
            }
        }

        return null;
    }
}
