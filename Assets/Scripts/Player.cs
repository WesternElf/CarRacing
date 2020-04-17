using System.Collections;
using Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float fuelCount = 100f;
        [SerializeField] private Image fuelImage;
        private float damping = 0.3f;
        private Rigidbody rigidbody;

        public float FuelCount => fuelCount;

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
                rigidbody.velocity = rigidbody.velocity * damping;
            }

        }

        private IEnumerator FuelChanging()
        {
            while (FuelCount > 0f)
            {
                fuelCount -= 1f;
                fuelImage.fillAmount = FuelCount / 100f;
                //Debug.Log(FuelCount);
                yield return new WaitForSeconds(1f);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Build"))
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
            fuelCount = Mathf.Clamp(FuelCount + 20f, 0f, 100f);
            //Debug.Log("New count: " + FuelCount);
        }

        public void Die()
        {
            Debug.Log("You died!");
        
        }

    }
}
