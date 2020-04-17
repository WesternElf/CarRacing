using System.Collections;
using UnityEngine;

namespace Scripts.SpawnFeatures
{
    public class SpawnPoint : MonoBehaviour
    {
        [Header("Spawn objects list")] [SerializeField]
        private GameObject[] spawnObject;
        [Header("Spawn attributes")]
        [SerializeField, Range(0.0f, 10.0f)]
        private int minSpawnDelay;
        [SerializeField, Range(0.0f, 10.0f)]
        private int maxSpawnDelay;
        [SerializeField, Tooltip("Place, where objects spawned")] private Transform spawnStartPoint;
        [SerializeField] private GameObject emptyObject;

        public Transform SpawnStartPoint => spawnStartPoint;
        public GameObject[] SpawnObject => spawnObject;
        public GameObject EmptyObject => emptyObject;

        private void Start()
        {
            StartCoroutine(StartSpawn());
        }

        protected IEnumerator StartSpawn()
        {
            while (true)
            {
                //Debug.Log("Spawn!");
                Spawner();
                yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            }
           
        }

        protected GameObject GetRandomObject()
        {
            var randomObject = SpawnObject[Random.Range(0, SpawnObject.Length)];
            //Debug.Log(randomObject.name);

            return randomObject;
        }

        protected virtual void Spawner()
        {
            var spawnObj = Instantiate(GetRandomObject(), SpawnStartPoint.position, SpawnStartPoint.rotation);
            spawnObj.transform.parent = EmptyObject.transform;
        }

    }
}
