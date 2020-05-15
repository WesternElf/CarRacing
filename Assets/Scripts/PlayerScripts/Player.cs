using System.Collections;
using ScriptableObjects;
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
        [SerializeField] private PlayerSkin playerSkin;
        [SerializeField] private GameObject mesh;

        private void Start()
        {
            _carMaterial = gameObject.GetComponentInChildren<MeshRenderer>().material;
            _rigidbody = GetComponent<Rigidbody>();
            InitializeValues();
            GetSkin("NormalCar");
            StartCoroutine(FuelChanging());
            UpdateManager.Instance.OnUpdateEvent += Movement;
            UpdateManager.Instance.OnUpdateEvent += ChangeSkin;
            //playerSkin.AssignAttributes(mesh, playerData.Speed, playerData.FuelCount);
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

        private void ChangeSkin()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                GetSkin("NormalCar");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                GetSkin("Bus");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                GetSkin("PoliceCar");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                GetSkin("TaxiCar");
            }
        }

        private void GetSkin(string skinName)
        {
            playerSkin = Resources.Load<PlayerSkin>($"CarSkins/{skinName}");
            playerData.Speed = playerSkin.Speed;
            playerData.FuelCount = playerSkin.FuelCount;
            _carMaterial.color = playerSkin.Color;

            if (mesh!=null)
            {
                Destroy(mesh);
            }
            mesh = Instantiate(playerSkin.Mesh, transform.position, transform.rotation);
            mesh.transform.parent = gameObject.transform;
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
            UpdateManager.Instance.OnUpdateEvent -= ChangeSkin;
        }
    }
}
