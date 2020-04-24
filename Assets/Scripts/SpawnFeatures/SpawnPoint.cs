using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpawnFeatures
{
    public class SpawnPoint : MonoBehaviour
    {
        [Header("Spawn attributes")]
        [SerializeField, Range(0f, 10f)] private int _minSpawnDelay;
        [SerializeField, Range(0f, 10f)] private int _maxSpawnDelay;
        [SerializeField, Tooltip("Place, where objects spawned")] protected Transform SpawnStartPoint;
        [SerializeField] private GameObject prefab;

        protected Coroutine spawningCoroutine;
        private ObjectPooler _objectPooler;

        private void Awake()
        {
            _objectPooler = new ObjectPooler(prefab, 5);
            spawningCoroutine = StartCoroutine(StartSpawn());
        }

        protected IEnumerator StartSpawn()
        {
            while (true)
            {
                SpawnObject();
                yield return new WaitForSeconds(Random.Range(_minSpawnDelay, _maxSpawnDelay));
            }
        }
        
        private void SpawnObject()
        {
            var pooledObject = _objectPooler.GetObject();
            pooledObject.transform.position = SpawnStartPoint.position;
            pooledObject.transform.rotation = SpawnStartPoint.rotation;
        }

        protected virtual Vector3 GetPositionForNewObject()
        {
            return SpawnStartPoint.position;
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
