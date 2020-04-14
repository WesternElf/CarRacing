using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDrivable 
{
    float FuelCount { get; }
    void Die();
    void TakeFuel();
}
