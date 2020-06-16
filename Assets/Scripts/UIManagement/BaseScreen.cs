using System;
using UnityEngine;

public abstract class BaseScreen : MonoBehaviour
{
    public event Action OnBaseScreenEvent;
    [SerializeField] private GameObject _screenPrefab;

    private void OnEnabled()
    {
        OnBaseScreenEvent?.Invoke();
    }

    
    private void OnDisabled()
    {
            
    }
}
