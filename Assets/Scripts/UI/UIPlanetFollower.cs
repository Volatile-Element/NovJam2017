using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlanetFollower : MonoBehaviour
{
    public Dictionary<Planet, UITrackedPlanet> Planets = new Dictionary<Planet, UITrackedPlanet>();

    public GameObject TrackerTemplate;

    private void Awake()
    {
        TrackerTemplate = transform.Find("Planet Tracker").gameObject;
        TrackerTemplate.SetActive(false);
    }

    private void Update()
    {
        var player = FindObjectOfType<Ship>();
        var planet = FindObjectOfType<Planet>();

        foreach (var asteroid in Planets)
        {
            var playerDistance = Vector3.Distance(player.transform.position, asteroid.Key.transform.position);
            var planetDistance = Vector3.Distance(planet.transform.position, asteroid.Key.transform.position);

            var visible = asteroid.Key.GetComponentInChildren<Renderer>().isVisible;

            asteroid.Value.UpdateDistance(playerDistance, visible);
        }
    }

    public void AddPlanet(Planet planet)
    {
        var tracker = Instantiate(TrackerTemplate);
        tracker.transform.parent = transform;
        tracker.SetActive(true);
        tracker.GetComponent<GUISceneFollower>().FollowThis = planet.transform;

        Planets.Add(planet, tracker.GetComponent<UITrackedPlanet>());
    }
}