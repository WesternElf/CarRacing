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
        private Transform _randomPoint;
        
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

        private void SpawnObject(ObjectPooler pool)
        {
            var pooledObject = pool.GetObject();
            var randomPoint = GetRandomPoint();
            pooledObject.transform.position = randomPoint.position;
            pooledObject.transform.rotation = randomPoint.rotation;
        }

        private Transform GetRandomPoint()
        {
            var newPoint = _points[Random.Range(0, _points.Count)];
            
            //Debug.Log("Old point: " + _randomPoint);
            //if (_randomPoint != newPoint)
            //{
            //    _randomPoint = newPoint;
            //}
            //else
            //{
            //    return GetRandomPoint();
            //}
            //Debug.Log("New point: " + _randomPoint);
            return newPoint;
        }
    }
}
