using System.Collections;
using Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerScripts
{
    public class Player : MonoBehaviour
    {
        private const string PetrolTag = "Petrol";

        private float _speed;
        private float _fuelCount;
        private Material _carMaterial;
        [SerializeField] private Image _fuelImage;
        private float damping = 0.3f;
        private Rigidbody _rigidbody;

        private void Start()
        {
            _carMaterial = gameObject.GetComponentInChildren<MeshRenderer>().material;
            _rigidbody = GetComponent<Rigidbody>();
            InitializeValues();
            StartCoroutine(FuelChanging());
            UpdateManager.Instance.OnUpdateEvent += Movement;
        }


        private void OnDestroy()
        {
            UpdateManager.Instance.OnUpdateEvent -= Movement;
        }


        private void InitializeValues()
        {
            _speed = PlayerData.NewSpeedValue;
            _fuelCount = PlayerData.NewFuelValue;
            _carMaterial.color = PlayerData.NewMaterial;
            print("Speed = " + _speed + " Fuel = " + _fuelCount + " Color " + _carMaterial.color);
        }


        private void Movement()
        {
            var horizontal = transform.right * Input.GetAxis("Horizontal");
            var vertical = transform.forward * Input.GetAxis("Vertical");

            var moving = horizontal + vertical;
            moving.Normalize();

            moving *= _speed;

            if (moving.magnitude > 0f)
            {
                _rigidbody.AddForce(moving);
            }
            else
            {
                _rigidbody.velocity *= damping;
            }
        }

        private IEnumerator FuelChanging()
        {
            while (_fuelCount > 0f)
            {
                yield return new WaitForSeconds(1f);
                _fuelCount -= 1f;
                _fuelImage.fillAmount = _fuelCount / 100f;
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(PetrolTag))
            {
                TakeFuel();
            }

            IDestructable destructable = other.gameObject.GetComponent<IDestructable>();
            if (destructable != null)
            {
                Die();
            }
        }


        private void TakeFuel()
        {
            _fuelCount = Mathf.Clamp(_fuelCount + 20f, 0f, 100f);
        }

        private void Die()
        {
            Debug.Log("You died!");
        }
    }
}
