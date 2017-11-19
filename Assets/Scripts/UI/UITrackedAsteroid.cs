using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITrackedAsteroid : MonoBehaviour
{
    public Text txtDistanceToPlanet;
    public Text txtDistanceToPlayer;

    private void Awake()
    {
        txtDistanceToPlanet = transform.Find("txtDistanceToPlanet").GetComponent<Text>();
        txtDistanceToPlayer = transform.Find("txtDistanceToPlayer").GetComponent<Text>();
    }

    public void UpdateDistance(float planetDistance, float playerDistance, bool visible)
    {
        if (visible)
        {
            gameObject.SetActive(true);

            txtDistanceToPlanet.text = planetDistance.ToString("#");
            txtDistanceToPlayer.text = playerDistance.ToString("#");
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}