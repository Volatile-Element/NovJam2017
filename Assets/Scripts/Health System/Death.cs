using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Death : MonoBehaviour
{
    public UnityEvent OnDeath = new UnityEvent();

    private void Awake()
    {
        GetComponent<Health>().OnHealthChanged.AddListener(OnHealthChanged);
    }

    public void OnHealthChanged(float currentHealth)
    {
        if (currentHealth <= 0)
        {
            OnDeath.Invoke();
        }
    }
}