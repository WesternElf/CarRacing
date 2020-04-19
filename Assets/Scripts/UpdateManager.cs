using System;
using UnityEngine;

namespace Scripts
{
    public class UpdateManager : MonoBehaviour
    {
        public event Action OnUpdateEvent;

        private static UpdateManager instance;

        public static UpdateManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<UpdateManager>();
                }

                if (instance == null)
                {
                    instance = new GameObject("UpdateManager", typeof(UpdateManager)).GetComponent<UpdateManager>();
                }

                return instance;
            }
        }

        private void Update()
        {
            OnUpdateEvent?.Invoke();
        }
    }
}
