using FullSerializer;
using UnityEngine;

namespace PlayerScripts
{
    [System.Serializable]
    public class PlayerData 
    {
        //[fsProperty("S")] 
        public float Speed = 12f;
        public float FuelCount = 100f;
        public Color CarMaterial = Color.red;
    }
}
