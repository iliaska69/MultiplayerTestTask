using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Image barImage;

    private void Start()
    {
        if(health == null) return;
        health.healthChangedEvent += OnHealthChanged;
    }

    private void OnDestroy()
    {
        if(health == null) return;
        health.healthChangedEvent -= OnHealthChanged;
    }

    private void OnHealthChanged()
    {
        var healthPercent = health.CurrentHealth / health.MaxHealth;
        barImage.fillAmount = healthPercent;
    }
}
