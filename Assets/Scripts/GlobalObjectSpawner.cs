using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class GlobalObjectSpawner : MonoBehaviour
    {
        [SerializeField] private ObjectSpawner[] _objectSpawner;


        void Start()
        {
            _objectSpawner = new ObjectSpawner[2];
            foreach (var objects in _objectSpawner)
            {
                objects.GetPool();
            }
            StartCoroutine(Spawner());

        }

        private IEnumerator Spawner()
        {
            while (true)
            {
                foreach (var objects in _objectSpawner)
                {
                    objects.Spawn();
                }
                yield return new WaitForSeconds(2);

            }
        }
    }
}
