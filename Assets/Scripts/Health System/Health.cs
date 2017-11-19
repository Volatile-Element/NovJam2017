using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public UnityEventFor<float> OnHealthChanged = new UnityEventFor<float>();

    private float _CurrentHealth;
    public float CurrentHealth { get { return _CurrentHealth; } }

    private float _MaxHealth;
    public float MaxHealth { get { return _MaxHealth; } }

    public void AddToCurrentHealth(float amount)
    {
        SetCurrentHealth(CurrentHealth + amount);
    }

    public void RemoveFromCurrentHealth(float amount)
    {
        SetCurrentHealth(CurrentHealth - amount);
    }

    public void SetCurrentHealth(float amount)
    {
        _CurrentHealth = amount;

        OnHealthChanged.Invoke(CurrentHealth);
    }

    public void SetMaxHealth(float amount, bool refillHealth)
    {
        _MaxHealth = amount;

        if (refillHealth)
        {
            SetCurrentHealth(MaxHealth);
        }
        else
        {
            SetCurrentHealth(Mathf.Min(_MaxHealth, _CurrentHealth));
        }
    }
}