using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private UIValueBarsHolder _barHolder;

    private void Awake()
    {
        _barHolder.Init(_health.CurrentHealth, _health.MaxHealth);
        _health.HealthIncreased += _barHolder.IncreaseValue;
        _health.HealthDecreased += _barHolder.DecreaseValue;
    }
}
