using System.Collections;
using Assets.Scripts.ObjectPooling;
using Extensions;
using Scripts.Interfaces;
using UnityEngine;

namespace SpawnFeatures
{ 
    public class SpawnObject : MonoBehaviour, IDestructable
    {
        [SerializeField] private float speed;
        [SerializeField] private float hitPoints;
        private float outPosZ = -25f;
        public float HitPoints => hitPoints;
        private const string targetName = "Player";
        
        private void OnValidate()
        {
            if(speed <= 0f)
            {
                speed = 0f;
            }
        }

        private void Start()
        {
            gameObject.RemoveCloneFromName();
            
        }

        private void OnEnable()
        {
            StartCoroutine(MoveSpawnObject());
        }

        private IEnumerator MoveSpawnObject()
        {
            while (gameObject.activeInHierarchy)
            {
               Move();
               yield return null;
            }
        }
       
        private void Move()
        {
            float objectPosZ = transform.position.z;
            var player = GameObject.Find(targetName);

            if (GameController.Instance.State == GameState.Play)
            {
                if (objectPosZ > outPosZ)
                {
                    transform.Translate(player.transform.right * (speed * Time.deltaTime));
                }
                else
                {
                    gameObject.GetComponent<PoolableObject>().ReturnToPool();
                }
            }
            
           
        }

    }
}
