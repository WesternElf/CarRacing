using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpawnFeatures
{
    public class SpawnPoint : MonoBehaviour
    {
        [Header("Spawn objects list")] 
        [SerializeField] private GameObject[] spawnObject;
        [Header("Spawn attributes")]
        [SerializeField, Range(0.0f, 10.0f)] private int minSpawnDelay;
        [SerializeField, Range(0.0f, 10.0f)] private int maxSpawnDelay;
        [SerializeField, Tooltip("Place, where objects spawned")] private Transform spawnStartPoint;
        [SerializeField] private GameObject emptyObject;

        private Coroutine spawningCoroutine;
        
        private void Start()
        {
            spawningCoroutine = StartCoroutine(StartSpawn());
        }

        private IEnumerator StartSpawn()
        {
            while (true)
            {
                SpawnRandomObject();
                yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            }
        }
        
        private void SpawnRandomObject()
        {
            Instantiate(GetRandomObject(), GetPositionForNewObject(), spawnStartPoint.rotation, emptyObject.transform);
        }

        protected virtual Vector3 GetPositionForNewObject()
        {
            return spawnStartPoint.position;
        }

        private GameObject GetRandomObject()
        {
            return spawnObject[Random.Range(0, spawnObject.Length)];
        }

        private void OnDestroy()
        {
            if (spawningCoroutine != null)
            {
                StopCoroutine(spawningCoroutine);
            }
        }
    }
}
