using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New PlayerSkin", menuName = "Player Skin", order = 51)]
    public class PlayerSkin : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Color _color;
        [SerializeField] private float _speed;
        [SerializeField] private float _fuelCount;

        public string Name => _name;
        public float FuelCount => _fuelCount;

        public Color AssignColor(Color color)
        {
            color = _color;
            return color;
        }

        public float AssignSpeed(float speed)
        {
            speed = _speed;
            return speed;
        }

        public float AssingnFuelCount(float fuelCount)
        {
            fuelCount = _fuelCount;
            return fuelCount;
        }

    }
}
