using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class GlobalObjectSpawner : MonoBehaviour
    {
        [SerializeField] private ObjectSpawner[] _objectSpawner;

        private void Start()
        {
            for(int i = 0; i < _objectSpawner.Length; i++)
            {
                _objectSpawner[i].Initialize();
            }

            StartCoroutine(Spawner());
        }

        private IEnumerator Spawner()
        {
            var delay = new WaitForSeconds(2f);

            while (true)
            {
                foreach (var objects in _objectSpawner)
                {
                    objects.Spawn();
                }
                yield return delay;
            }
        }
    }
}
