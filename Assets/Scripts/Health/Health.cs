using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth;
    [SerializeField] private UnityEvent healthIsOverEvent;
    [SerializeField] private UnityEvent<float> damageTakenEvent;

    public float MaxHealth => maxHealth;
    public float CurrentHealth => _currentHealth;
    
    public delegate void HealthChanged();
    public event HealthChanged healthChangedEvent;
    
    private float _currentHealth;

    private void Start()
    {
        _currentHealth = maxHealth;
    }

    public void Damage(float damageValue)
    {
        damageTakenEvent.Invoke(damageValue);
        _currentHealth -= damageValue;
        healthChangedEvent?.Invoke();
        if(_currentHealth > 0) return;
        healthIsOverEvent.Invoke();
        _currentHealth = 0;
    }
    
}
