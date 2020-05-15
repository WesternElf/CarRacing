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
        [SerializeField] private GameObject _mesh;

        public string Name => _name;
        public float FuelCount => _fuelCount;
        public Color Color => _color;
        public float Speed => _speed;
        public GameObject Mesh => _mesh;


    }
}
