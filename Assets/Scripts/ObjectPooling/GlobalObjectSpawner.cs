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

                StartCoroutine(_objectSpawner[i].Spawner());
            }

            //foreach (var objects in _objectSpawner)
            //{
            //    StartCoroutine(objects.Spawner());
            //}
        }

        //private IEnumerator Spawner()
        //{
        //    var delay = new WaitForSeconds(1f);

        //    while (true)
        //    {
        //        foreach (var objects in _objectSpawner)
        //        {
        //            objects.Spawn();
        //        }
        //        yield return delay;
        //    }
        //}
    }
}
