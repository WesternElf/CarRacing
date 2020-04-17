using UnityEngine;

namespace Scripts.SpawnFeatures
{
    public class CarRandomize : SpawnPoint
    {
        [Header("Car start position settings")]
        [SerializeField] private float posX;
        [SerializeField, Range(-15f, 15.0f)] private float minPosX;
        [SerializeField, Range(-15f, 15.0f)] private float maxPosX;

        private void Start()
        {
            StartCoroutine(StartSpawn());
        }

        protected override void Spawner()
        {
            posX = Random.Range(minPosX, maxPosX);
            var spawnPos = new Vector3(posX, SpawnStartPoint.position.y, SpawnStartPoint.position.z);

            GameObject newObject = Instantiate(GetRandomObject(), spawnPos, SpawnStartPoint.rotation);
            newObject.transform.parent = EmptyObject.transform;
        }
    }
}
