using UnityEngine;

namespace PlayerScripts
{
    public class PlayerData 
    {
        public float Speed;
        public float FuelCount;
        public Color CarMaterial;

        public static float NewSpeedValue = 0;
        public static float NewFuelValue = 0;
        public static Color NewMaterial;

        public PlayerData(float speed, float fuelCount, Color color)
        {
            this.FuelCount = fuelCount;
            this.Speed = speed;
            this.CarMaterial = color;
        }
    }
}
