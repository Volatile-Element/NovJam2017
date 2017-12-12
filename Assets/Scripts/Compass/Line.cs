using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public Transform ActivePlayer;
    public Transform Planet;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        Planet = FindObjectOfType<Planet>().transform;
    }

    private void FixedUpdate()
    {
        lineRenderer.SetPosition(0, ActivePlayer.transform.position);
        lineRenderer.SetPosition(1, Planet.transform.position);
    }

    public void SetActivePlayer(Transform activePlayer)
    {
        ActivePlayer = activePlayer;
    }
}