using System;
using System.Collections.Generic;
using System.Collections;
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
        [SerializeField] private float delay;

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

        public IEnumerator Spawner()
        {
            while (true)
            {
                Spawn();
                yield return new WaitForSeconds(delay);
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
            pooledObject.SetActive(true);
        }

        private Transform GetRandomPoint()
        {
            var newPoint = _points[Random.Range(0, _points.Count)];

            if (_points.Count > 1)
            {
                if (_randomPoint != newPoint)
                {
                    _randomPoint = newPoint;
                }
                else
                {
                    return GetRandomPoint();
                }
            }
            else
            {
                newPoint = _points[0];
            }
            return newPoint;
        }
    }
}
