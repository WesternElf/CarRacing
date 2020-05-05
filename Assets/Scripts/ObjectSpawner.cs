using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    [Serializable]
    public class ObjectSpawner
    {
        [SerializeField] private int objectsCount;
        [SerializeField] private List<Transform> _points;
        [SerializeField] private List<GameObject> _possibleObjects;

        private ObjectPooler[] _pools;

        public void Initialize()
        {
            _pools = new ObjectPooler[_possibleObjects.Count];

            for (int i = 0; i < _pools.Length; i++)
            {
                _pools[i] = new ObjectPooler(_possibleObjects[i], objectsCount);
            }
        }

        public void Spawn()
        {
            var randomPool = _pools[Random.Range(0, _pools.Length - 1)];
            SpawnObject(randomPool);
        }

        public void SpawnObject(ObjectPooler pool)
        {
            var pooledObject = pool.GetObject();
            var randomPoint = GetRandomPoint();
            pooledObject.transform.position = randomPoint.position;
            pooledObject.transform.rotation = randomPoint.rotation;
        }

        private Transform GetRandomPoint()
        {
            return _points[Random.Range(0, _points.Count - 1)];
        }
    }
}
