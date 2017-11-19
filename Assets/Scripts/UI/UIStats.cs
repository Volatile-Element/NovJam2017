using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStats : MonoBehaviour
{
    public Text txtShipThrottle;
    public Text txtShipVelocity;
    public Text txtPlanetIntegrity;

    private void Awake()
    {
        txtShipThrottle = transform.Find("Stat Collection/txtShipThrottle").GetComponent<Text>();
        txtShipVelocity = transform.Find("Stat Collection/txtShipVelocity").GetComponent<Text>();
        txtPlanetIntegrity = transform.Find("Stat Collection/txtPlanetIntegrity").GetComponent<Text>();
    }

    private void Start()
    {
        FindObjectOfType<Ship>().OnThrottleChanged.AddListener(OnThrottleChanged);
        FindObjectOfType<Ship>().OnVelocityChanged.AddListener(OnVelocityChanged);
        FindObjectOfType<Planet>().GetComponent<Health>().OnHealthChanged.AddListener(OnHealthChanged);
    }


    public void OnThrottleChanged(float currentThrottle)
    {
        txtShipThrottle.text = $"Throttle: {currentThrottle}";
    }

    public void OnVelocityChanged(string currentVelocity)
    {
        txtShipVelocity.text = $"Velocity: {currentVelocity}";
    }


    public void OnHealthChanged(float currentHealth)
    {
        txtPlanetIntegrity.text = $"Planet Integrity: {currentHealth}%";
    }
}