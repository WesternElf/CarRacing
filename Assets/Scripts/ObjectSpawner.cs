using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    [Serializable]
    public class ObjectSpawner
    {
        [SerializeField] private List<Transform> _points;
        [SerializeField] private List<GameObject> _possibleObjects;
        [SerializeField] private int objectsCount;

        private ObjectPooler[] _pools;
        private GameObject obj;

        public ObjectSpawner()
        {


        }

        
        public ObjectPooler[] GetPool()
        {
            _pools = new ObjectPooler[2];
            for (int i = 0; i < _pools.Length; i++)
            {
                _pools[i] = new ObjectPooler(GetGameObject(), objectsCount);
                SpawnObject(_pools[i]);
            }

            return _pools;
        }

        public void Spawn()
        {
            ObjectPooler[] pools = GetPool();
            for (int i = 0; i < pools.Length; i++)
            {
                SpawnObject(pools[i]);
            }
        }

        public void SpawnObject(ObjectPooler pool)
        {

            var pooledObject = pool.GetObject();
            pooledObject.transform.position = GetTransform().position;
            pooledObject.transform.rotation = GetTransform().rotation;

            Debug.Log(pooledObject.name + pooledObject.transform.position);
        }

        private GameObject GetGameObject()
        {
            _possibleObjects = new List<GameObject>();
            obj = _possibleObjects[Random.Range(0, _possibleObjects.Count)];
            return obj;
        }

        private Transform GetTransform()
        {
            _points = new List<Transform>();
            Transform newPosition = _points[Random.Range(0, _points.Count - 1)];
            return newPosition;
        }


    }
}
