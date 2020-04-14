using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRandomize : SpawnPont
{
    [Header("Car start position settings")]
    [SerializeField] float posX;
    [SerializeField, Range(-15f, 15.0f)] float minPosX;
    [SerializeField, Range(-15f, 15.0f)] float maxPosX;


    void Start()
    {
        posX = SpawnStartPoint.position.x;
        InvokeRepeating("Spawner", 0.0f, 2.0f);
    }

    public override void Spawner()
    {
        for (int i = 0; i < SpawnObject.Count; i++)
        {
            RandomObject = SpawnObject[Random.Range(0, SpawnObject.Count)];

            Debug.Log(SpawnObject[i].name);
        }
        posX = Random.Range(minPosX, maxPosX);
        Instantiate(RandomObject, new Vector3(posX, SpawnStartPoint.position.y, SpawnStartPoint.position.z), SpawnStartPoint.rotation);
    }
}
