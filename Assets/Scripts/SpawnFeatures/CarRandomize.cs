using UnityEngine;

namespace SpawnFeatures
{
    public class CarRandomize : SpawnPoint
    {
        [Header("Car start position settings")]
        [SerializeField, Range(-15f, 15f)] private float minPosX;
        [SerializeField, Range(-15f, 15f)] private float maxPosX;

        protected override Vector3 GetPositionForNewObject()
        {
            var position = base.GetPositionForNewObject();
            position.x = Random.Range(minPosX, maxPosX);
            return position;
        }
    }
}
