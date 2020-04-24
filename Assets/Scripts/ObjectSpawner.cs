using System;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class ObjectSpawner
    {
        [SerializeField] private Transform[] points;
        [SerializeField] private GameObject[] possibleObjects;
        
        private ObjectPooler[] pools;

        public ObjectSpawner()
        {

        }

        public GameObject GetGameObject()
        {
            return null;
        }
    }
}
