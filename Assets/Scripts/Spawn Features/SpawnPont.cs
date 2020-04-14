using System.Collections.Generic;
using UnityEngine;


public class SpawnPont : MonoBehaviour
{
    [Header("Spawn objects list")]
    [SerializeField] private List <GameObject> spawnObject = new List<GameObject>();
    [Header("Spawn attributes")]
    [SerializeField, Range(0.0f, 10.0f)]
    private int minSpawnDelay;
    [SerializeField, Range(0.0f, 10.0f)]
    private int maxSpawnDelay;
    [SerializeField, Range(0.0f, 10.0f)]
    private int startSpawnTime;
    [Space(20)]
    [SerializeField, Tooltip("Place, where objects spawned")] private Transform spawnStartPoint;
    private GameObject randomObject;

    public Transform SpawnStartPoint { get => spawnStartPoint; }
    public GameObject RandomObject { get => randomObject; set => randomObject = value; }
    public List<GameObject> SpawnObject { get => spawnObject; }


    private void Start()
    {
        InvokeRepeating("Spawner", startSpawnTime, Random.Range(minSpawnDelay, maxSpawnDelay));
   
    }

    public virtual void Spawner()
    {
        for(int i = 0; i < SpawnObject.Count; i++)
        {
            RandomObject = SpawnObject[Random.Range(0, SpawnObject.Count)];
            
            Debug.Log(SpawnObject[i].name);
        }
        
        Instantiate(RandomObject, SpawnStartPoint.position, SpawnStartPoint.rotation);

    }

}
