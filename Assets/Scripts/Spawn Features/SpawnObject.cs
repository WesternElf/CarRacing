
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpawnObject : MonoBehaviour, IDestructable
{
    [SerializeField] private float dividerSpeed;
    [SerializeField] private float hitPoints;

    public float HitPoints { get => hitPoints; set => hitPoints = value; }

    public float DividerSpeed { get => dividerSpeed;  }

    private void OnValidate()
    {
        if(dividerSpeed <= 0)
        {
            dividerSpeed = 0;
        }
    }



    private void Update()
    {
        if (gameObject.CompareTag("Build") || gameObject.CompareTag("Car"))
            transform.Translate(Vector3.right * DividerSpeed * Time.deltaTime);
        else if (gameObject.CompareTag("Right Car"))
            transform.Translate(Vector3.left * DividerSpeed * Time.deltaTime);
        else
            transform.Translate(Vector3.back * DividerSpeed * Time.deltaTime);
        
        if (transform.position.z < -22f)
        {
            Destroy(gameObject);
        }
    }
}
