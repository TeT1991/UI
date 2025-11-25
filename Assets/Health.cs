using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float _currentHealth = 10;
    private float _maxHealth = 100;

    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;

    public event Action<float> HealthIncreased;
    public event Action<float> HealthDecreased;

    public void IncreaseHealth(float value)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + value, 0, _maxHealth);
        HealthIncreased?.Invoke(_currentHealth);
    }

    public void DecreaseHealth(float value)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - value, 0, _maxHealth);
        HealthDecreased?.Invoke(_currentHealth);
    }
}
