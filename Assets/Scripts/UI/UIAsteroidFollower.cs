using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAsteroidFollower : MonoBehaviour
{
    public Dictionary<Asteroid, UITrackedAsteroid> Asteroids = new Dictionary<Asteroid, UITrackedAsteroid>();

    public GameObject TrackerTemplate;

    private void Awake()
    {
        TrackerTemplate = transform.Find("Asteroid Tracker").gameObject;
        TrackerTemplate.SetActive(false);
    }

    private void Update()
    {
        var player = FindObjectOfType<Ship>();
        var planet = FindObjectOfType<Planet>();

        foreach (var asteroid in Asteroids)
        {
            var playerDistance = Vector3.Distance(player.transform.position, asteroid.Key.transform.position);
            var planetDistance = Vector3.Distance(planet.transform.position, asteroid.Key.transform.position);

            var visible = asteroid.Key.GetComponent<Renderer>().isVisible;

            asteroid.Value.UpdateDistance(planetDistance, playerDistance, visible);
        }
    }

    public void AddAsteroid(Asteroid asteroid)
    {
        var tracker = Instantiate(TrackerTemplate);
        tracker.transform.parent = transform;
        tracker.SetActive(true);
        tracker.GetComponent<GUISceneFollower>().FollowThis = asteroid.transform;

        Asteroids.Add(asteroid, tracker.GetComponent<UITrackedAsteroid>());
    }

    public void RemoveAsteroid(Asteroid asteroid)
    {
        Destroy(Asteroids[asteroid]);

        Asteroids.Remove(asteroid);
    }
}