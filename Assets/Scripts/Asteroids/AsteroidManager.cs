using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    public Asteroid[] AsteroidPrefabs = new Asteroid[0];
    private Transform AsteroidParent;

    private void Awake()
    {
        AsteroidParent = new GameObject("Asteroid Container").transform;
    }

    public void CreateAsteroid(Vector3 minExtents, Vector3 maxExtents)
    {
        var prefab = AsteroidPrefabs[Random.Range(0, AsteroidPrefabs.Length)];
        var spawnLocation = GetLocationFromExents(minExtents, maxExtents);
        var scale = Random.Range(50, 100);

        var asteroid = CreateAsteroid(spawnLocation, scale);

        var currentForce = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(-100, 100));
        asteroid.GetComponent<Rigidbody>().AddForce(currentForce);
    }

    public Asteroid CreateAsteroid(Vector3 spawnLocation, float scale)
    {
        var prefab = AsteroidPrefabs[Random.Range(0, AsteroidPrefabs.Length)];

        var asteroid = Instantiate(prefab, spawnLocation, Quaternion.identity, AsteroidParent).GetComponent<Asteroid>();
        asteroid.UpdateModelScale(Vector3.one * scale);
        asteroid.GetComponentInChildren<LandableBody>().SetScale(Vector3.one * scale);
        asteroid.GetComponent<Health>().SetMaxHealth(10 * scale, true);
        
        asteroid.GetComponent<Rigidbody>().mass = scale;
        
        AddToUI(asteroid);

        return asteroid;
    }

    private Vector3 GetLocationFromExents(Vector3 minExtents, Vector3 maxExtents)
    {
        var x = Random.Range(minExtents.x, maxExtents.x);
        var y = Random.Range(minExtents.y, maxExtents.y);
        var z = Random.Range(minExtents.z, maxExtents.z);

        var axisToSetAtMax = Random.Range(0, 3);
        switch (axisToSetAtMax)
        {
            case 0:
                x = Random.Range(0, 2) == 0 ? minExtents.x : maxExtents.x;
                break;
            case 1:
                y = Random.Range(0, 2) == 0 ? minExtents.y : maxExtents.y;
                break;
            case 2:
                z = Random.Range(0, 2) == 0 ? minExtents.z : maxExtents.z;
                break;
        }
        
        return new Vector3(x, y, z);
    }

    public void DestroyAsteroid(Asteroid asteroid)
    {
        Destroy(asteroid);
    }

    private void AddToUI(Asteroid asteroid)
    {
        FindObjectOfType<UIAsteroidFollower>().AddAsteroid(asteroid);
    }

    private void RemoveFromUI(Asteroid asteroid)
    {
        FindObjectOfType<UIAsteroidFollower>().RemoveAsteroid(asteroid);
    }
}