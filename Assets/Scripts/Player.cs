using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts
{
    public class Player : MonoBehaviour, IDrivable
    {
        [SerializeField] private float speed;
        [SerializeField] private float fuelCount = 100;
        [SerializeField] private Image fuelImage;
        private float damping = 0.3f;
        private Rigidbody rigidbody;

        public float FuelCount => fuelCount;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            fuelImage.fillAmount = FuelCount / 100;
            StartCoroutine(FuelChanging());
        }

        private void Update()
        {
            fuelImage.fillAmount = FuelCount / 100;
            Movement();
        }

        private void Movement()
        {
            Vector3 horizontal = transform.right * Input.GetAxis("Horizontal");
            Vector3 vertical = transform.forward * Input.GetAxis("Vertical");

            Vector3 moving = horizontal + vertical;
            moving.Normalize();

            moving *= speed;

            if (moving.magnitude > 0)
            {
                rigidbody.AddForce(moving);
            }
            else
            {
                rigidbody.velocity = rigidbody.velocity * damping;
            }

        }

        public void Die()
        {
            Debug.Log("Catch!");
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Build"))
            {
                TakeFuel();
            }

            IDestructable destructable = other.gameObject.GetComponent<IDestructable>();
            if(destructable != null)
            {
                Die();
            }
        }
        
        public void TakeFuel()
        {
            fuelCount = Mathf.Clamp(FuelCount + 20, 0, 100);
            //Debug.Log("New count: " + FuelCount);
        }

        private IEnumerator FuelChanging()
        {
            while (FuelCount > 0)
            {
                fuelCount -= 2;
                Debug.Log(FuelCount);
                yield return new WaitForSeconds(1f);
            }
  
        }
    }
}
