using System.Collections;
using Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class Player : MonoBehaviour
    {
        private const string BuildTag = "Build";
        
        [SerializeField] private float speed;
        [SerializeField] private float fuelCount = 100f;
        [SerializeField] private Image fuelImage;
        private float damping = 0.3f;
        private Rigidbody rigidbody;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            StartCoroutine(FuelChanging());
        }

        private void Update()
        {
            Movement();
        }

        private void Movement()
        {
            var horizontal = transform.right * Input.GetAxis("Horizontal");
            var vertical = transform.forward * Input.GetAxis("Vertical");

            var moving = horizontal + vertical;
            moving.Normalize();

            moving *= speed;

            if (moving.magnitude > 0f)
            {
                rigidbody.AddForce(moving);
            }
            else
            {
                rigidbody.velocity *= damping;
            }
        }

        private IEnumerator FuelChanging()
        {
            while (fuelCount > 0f)
            {
                fuelCount -= 1f;
                fuelImage.fillAmount = fuelCount / 100f;
                yield return new WaitForSeconds(1f);
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(BuildTag))
            {
                TakeFuel();
            }

            IDestructable destructable = other.gameObject.GetComponent<IDestructable>();
            if (destructable != null)
            {
                Die();
            }
        }

        public void TakeFuel()
        {
            fuelCount = Mathf.Clamp(fuelCount + 20f, 0f, 100f);
        }

        public void Die()
        {
            Debug.Log("You died!");
        }
    }
}
