using System.Collections;
using System.Data.SqlTypes;
using ScriptableObjects;
using Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerScripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _fuelCount = 100f;
        [SerializeField] private GameObject _mesh;
        private const string PetrolTag = "Petrol";
        private Material _carMaterial;
        private float damping = 0.3f;
        private Rigidbody _rigidbody;
        [SerializeField] private Image _fuelImage;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private PlayerSkin playerSkin;

        private void Start()
        {
            _carMaterial = gameObject.GetComponentInChildren<MeshRenderer>().material;
            _rigidbody = GetComponent<Rigidbody>();
            InitializeValues();
            StartCoroutine(FuelChanging());

            UpdateManager.Instance.OnUpdateEvent += Movement;
        }

        public void InitializeValues()
        {
            var loadedData = LoadSaveData.LoadPlayer();

            if(loadedData != null)
            {
                playerData = loadedData;
            }
            else
            {
                playerData = new PlayerData();
            }

            playerSkin = Resources.Load<PlayerSkin>($"CarSkins/{playerData.SkinName}");
   
            //_carMaterial.color = playerData.CarMaterial;

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

        private void OnDestroy()
        {
            LoadSaveData.SavePlayer(playerData);
            UpdateManager.Instance.OnUpdateEvent -= Movement;
        }
    }
}
