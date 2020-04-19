using UnityEngine;
using System.Collections.Generic;

namespace ObjectPooling
{
    public class ObjectPooling : MonoBehaviour
    {
        public GameObject poolObject;
        public int amount;
        public bool instantiateOnStart = true;
        public bool overAmount = true;
        private  List<GameObject> pool = new List<GameObject>();

        private void Start()
        {
            if (instantiateOnStart && poolObject != null && amount > 0)
            {
                var instance = Instantiate(poolObject) as GameObject;
                instance.SetActive(false);
                pool.Add(instance);
            }
        }

        public GameObject InstantiateObject(Vector3 position, Quaternion rotation)
        {
            foreach (var item in pool)
            {
                if (!item.activeInHierarchy)
                {
                    item.transform.position = position;
                    item.transform.rotation = rotation;
                    item.SetActive(true);
                    return item;
                }
            }

            if (overAmount)
            {
                var instance = Instantiate(poolObject, position, rotation) as GameObject;
                pool.Add(instance);
                return instance;
            }

            return null;
        }
    }
}
