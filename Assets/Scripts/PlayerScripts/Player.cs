using System.Collections;
using Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerScripts
{
    public class Player : MonoBehaviour
    {
        private const string PetrolTag = "Petrol";

        private Material _carMaterial;
        [SerializeField] private Image _fuelImage;
        private float damping = 0.3f;
        private Rigidbody _rigidbody;
        [SerializeField] private PlayerData playerData;

        private void Start()
        {
            _carMaterial = gameObject.GetComponentInChildren<MeshRenderer>().material;
            _rigidbody = GetComponent<Rigidbody>();
            InitializeValues();
            StartCoroutine(FuelChanging());
            UpdateManager.Instance.OnUpdateEvent += Movement;
        }

        private void InitializeValues()
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

            _carMaterial.color = playerData.CarMaterial;
        }

        private void Movement()
        {
            var horizontal = transform.right * Input.GetAxis("Horizontal");
            var vertical = transform.forward * Input.GetAxis("Vertical");

            var moving = horizontal + vertical;
            moving.Normalize();

            moving *= playerData.Speed;

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
            while (playerData.FuelCount > 0f)
            {
                yield return new WaitForSeconds(1f);
                playerData.FuelCount -= 1f;
                _fuelImage.fillAmount = playerData.FuelCount / 100f;
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
            playerData.FuelCount = Mathf.Clamp(playerData.FuelCount + 20f, 0f, 100f);
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
