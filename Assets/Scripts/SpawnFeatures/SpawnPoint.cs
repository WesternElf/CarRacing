using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpawnFeatures
{
    public class SpawnPoint : MonoBehaviour
    {

        [Header("Spawn attributes")]
        [SerializeField, Range(0.0f, 10.0f)] private int _minSpawnDelay;
        [SerializeField, Range(0.0f, 10.0f)] private int _maxSpawnDelay;
        [SerializeField, Tooltip("Place, where objects spawned")] protected Transform SpawnStartPoint;
        [SerializeField] protected GameObject EmptyObject;

        protected Coroutine spawningCoroutine;
        


        protected IEnumerator StartSpawn()
        {
            while (true)
            {
                
                SpawnObject();
                yield return new WaitForSeconds(Random.Range(_minSpawnDelay, _maxSpawnDelay));
            }
        }
        
        //private void SpawnRandomObject()
        //{
        //    Instantiate(GetRandomObject(), GetPositionForNewObject(), spawnStartPoint.rotation, emptyObject.transform);

        //}

        private void SpawnObject()
        {
            Debug.Log("Pool");
            GameObject poolObject = ObjectPooler.Instance.GetPoolObject();
            print(poolObject.name);
            if (poolObject != null)
            {
                poolObject.SetActive(true);
            }
        }

        protected virtual Vector3 GetPositionForNewObject()
        {
            return SpawnStartPoint.position;
        }

        //private GameObject GetRandomObject()
        //{
        //    return spawnObject[Random.Range(0, spawnObject.Length)];
        //}

        private void OnDestroy()
        {
            if (spawningCoroutine != null)
            {
                StopCoroutine(spawningCoroutine);
            }
        }
    }
}
