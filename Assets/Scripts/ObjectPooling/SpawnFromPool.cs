using System.Collections;
using UnityEngine;

namespace ObjectPooling
{
    public class SpawnFromPool : MonoBehaviour
    {
        [Header("Spawn attributes")]
        [SerializeField, Range(0.0f, 10.0f)] private int minSpawnDelay;
        [SerializeField, Range(0.0f, 10.0f)] private int maxSpawnDelay;
        [SerializeField, Tooltip("Place, where objects spawned")] private Transform spawnStartPoint;

        private IEnumerator StartSpawn()
        {
            while (true)
            {
                SpawnObject();
                yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            }
        }

        private void SpawnObject()
        {
            GameObject poolObject = ObjectPooler.Instance.GetPoolObject();
            if (poolObject != null)
            {
                poolObject.transform.position = GetPositionForNewObject();
                poolObject.transform.rotation = spawnStartPoint.rotation;

                poolObject.SetActive(true);
            }
        }


        protected virtual Vector3 GetPositionForNewObject()
        {
            return spawnStartPoint.position;
        }
    }
}
