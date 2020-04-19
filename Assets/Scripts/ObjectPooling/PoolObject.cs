using UnityEngine;

namespace Scripts.ObjectPooling
{
    public class PoolObject : MonoBehaviour
    {
        public void ReturnToPool()
        {
            gameObject.SetActive(false);
        }
    }
}
