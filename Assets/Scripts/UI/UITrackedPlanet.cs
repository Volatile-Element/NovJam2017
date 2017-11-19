using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITrackedPlanet : MonoBehaviour
{
    public Text txtDistanceToPlayer;

    private void Awake()
    {
        txtDistanceToPlayer = transform.Find("txtDistanceToPlayer").GetComponent<Text>();
    }

    public void UpdateDistance(float playerDistance, bool visible)
    {
        if (visible)
        {
            gameObject.SetActive(true);

            txtDistanceToPlayer.text = playerDistance.ToString("#");
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}