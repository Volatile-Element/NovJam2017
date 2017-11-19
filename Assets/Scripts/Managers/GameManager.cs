using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ship ShipPrefab;
    public Planet PlanetPrefab;
    public Player PlayerPrefab;

    private float Extent = 2000f;

    private List<Player> players = new List<Player>();
    private List<Planet> planets = new List<Planet>();
 
    private void Awake()
    {
        var startingPlayers = new[]
        {
            new {
                Planet = Instantiate(PlanetPrefab),
                Ship = Instantiate(ShipPrefab),
                Player = Instantiate(PlayerPrefab),
                PlanetPosition = new Vector3(0, 0, 5000)
            }
        };

        foreach (var player in startingPlayers)
        {
            player.Planet.transform.position = player.PlanetPosition;
            player.Ship.transform.position = player.PlanetPosition + player.Planet.GetComponentInChildren<LandableBody>().ShipLandingPoint;
            player.Player.transform.position = player.PlanetPosition + player.Planet.GetComponentInChildren<LandableBody>().ShipLandingPoint;
            player.Ship.ParentToGravity(player.Planet.GetComponent<Gravity>());
            player.Ship.GetComponentInChildren<Camera>().gameObject.SetActive(false);
            player.Player.ParentToGravity(player.Planet.GetComponent<Gravity>());

            players.Add(player.Player);
            planets.Add(player.Planet);
        }
    }

	// Use this for initialization
	void Start ()
    {
        CreateAsteroids();

        foreach (var planet in planets)
        {
            FindObjectOfType<UIPlanetFollower>().AddPlanet(planet);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateAsteroids()
    {
        for (int i = 0; i < 10; i++)
        {
            FindObjectOfType<AsteroidManager>().CreateAsteroid(new Vector3(0, 0, 5000) + (Vector3.one * -Extent), new Vector3(0, 0, 5000) + (Vector3.one * Extent));
        }
    }
}