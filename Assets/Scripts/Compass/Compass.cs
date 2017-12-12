using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    public Transform ActivePlayer;
    public Transform Planet;

    private void Start()
    {
        Planet = FindObjectOfType<Planet>().transform;
    }

	private void FixedUpdate()
    {
        Vector3 relativePos = Planet.position - ActivePlayer.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = rotation;
    }

    public void SetActivePlayer(Transform activePlayer)
    {
        ActivePlayer = activePlayer;
    }
}