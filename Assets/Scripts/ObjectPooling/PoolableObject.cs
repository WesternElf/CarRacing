using UnityEngine;

namespace Assets.Scripts.ObjectPooling
{
    public class PoolableObject : MonoBehaviour
    {
        private ObjectPooler _pooler;

        public void SetPool(ObjectPooler pooler)
        {
            _pooler = pooler;
        }

        public void ReturnToPool()
        {
            _pooler.ReturnGameObject(gameObject);
        }
    }
}
