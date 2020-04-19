using System.Collections;
using Scripts.Interfaces;
using UnityEngine;

namespace SpawnFeatures
{
    [RequireComponent(typeof(Rigidbody))]
    public class SpawnObject : MonoBehaviour, IDestructable
    {
        [SerializeField] private float speed;
        [SerializeField] private float hitPoints;
        [SerializeField] private GameObject player;
        private float outPosZ = -20f;
        public float HitPoints => hitPoints;
        
        private void OnValidate()
        {
            if(speed <= 0f)
            {
                speed = 0f;
            }
        }

        private void Start()
        {
            StartCoroutine(MoveSpawnObject());
        }

        private IEnumerator MoveSpawnObject()
        {
            while (true)
            {
                Move();
                yield return null;
            }
        }
       
        private void Move()
        {
            float objectPosZ = transform.position.z;

            if (objectPosZ > outPosZ)
            {
                transform.Translate(player.transform.right * (speed * Time.deltaTime));
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
