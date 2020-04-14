using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDrivable
{
    [SerializeField] private float speed;
    [SerializeField] private float fuelCount = 100;
    [SerializeField] private Image fuelImage;
    private float damping = 0.3f;
    private Rigidbody rb;

    public float FuelCount { get => fuelCount; set => fuelCount = value; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        fuelImage.fillAmount = FuelCount / 100;
    }

    private void Update()
    {
        FuelCount -= Time.deltaTime;
        fuelImage.fillAmount = FuelCount / 100;
        //Debug.Log(FuelCount);
        if (FuelCount == 0)
        {
            Destroy(gameObject);
        }
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
            rb.AddForce(moving);
        }
        else
        {
            rb.velocity = rb.velocity * damping;
            Vector3 dampedVelocity = rb.velocity;
            dampedVelocity.y = rb.velocity.y;

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
            if(FuelCount < 100)
            {
                TakeFuel();
            }
            

        }
        if(FuelCount >= 100)
        {
            FuelCount = 100;
        }

        IDestructable destructable = other.gameObject.GetComponent<IDestructable>();
        if(destructable != null)
        {
            Die();
        }
    }

    public void TakeFuel()
    {
        //Debug.Log("Add fuel");
        FuelCount += 20;

        //Debug.Log("New count: " + FuelCount);
    }
}
