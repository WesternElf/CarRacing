using System.Collections;
using ScriptableObjects;
using Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerScripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _fuelCount = 100f;
        [SerializeField] private GameObject _mesh;
        [SerializeField] private GameObject _fuelImage;
        [SerializeField] private PlayerData playerData;

        private float damping = 0.3f;
        private Rigidbody _rigidbody;
        private Transform playerTransform;

        public PlayerSkin newPlayerSkin;

        private const string PetrolTag = "Petrol";

        private void OnEnable()
        {
            playerTransform = gameObject.GetComponentInChildren<Transform>();
            _rigidbody = GetComponent<Rigidbody>();
            GetSkinData();
            StartCoroutine(FuelChanging());
            UpdateManager.Instance.OnUpdateEvent += Movement;
            
        }

        private void GetSkinData()
        {
            newPlayerSkin = ButtonSkin.NewSkin;
            print("PlayerSkin name " + newPlayerSkin.Name);
            _speed = newPlayerSkin.Speed;
            _fuelCount = newPlayerSkin.FuelCount;
            _mesh = newPlayerSkin.Mesh;
            var newMesh = Instantiate(_mesh, playerTransform.position, playerTransform.rotation, gameObject.transform);
        }

        //public void InitializeValues()
        //{
        //    var loadedData = LoadSaveData.LoadPlayer();

        //    if(loadedData != null)
        //    {
        //        playerData = loadedData;
        //    }
        //    else
        //    {
        //        playerData = new PlayerData();
        //    }

        //}

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
            var image = GameObject.Find("FuelImage");
            var fillImage = image.GetComponent<Image>().fillAmount;
            while (_fuelCount > 0f)
            {
                yield return new WaitForSeconds(1f);
                _fuelCount -= 1f;
                fillImage = _fuelCount / 100f;
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
