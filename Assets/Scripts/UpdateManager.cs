using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class UpdateManager
    {
        public delegate void UpdateMethodHandler();
        public  event UpdateMethodHandler OnUpdateMethod;

        private static UpdateManager instance;
    
        public static UpdateManager Instance => instance;

        private void Awake()
        {
            instance = this;
        }

        public void Update()
        {
            if (OnUpdateMethod!=null)
            {
                OnUpdateMethod();
            }

        }

    }
}
