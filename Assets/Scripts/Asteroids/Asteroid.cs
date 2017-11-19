using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Transform model;
    private SphereCollider sphereCollider;

    private void Awake()
    {
        model = transform.Find("Model");
        sphereCollider = GetComponent<SphereCollider>();
    }
    
    public void UpdateModelScale(Vector3 scale)
    {
        model.localScale = scale;
        sphereCollider.radius = model.localScale.x / 2;
    }

    void OnCollisionEnter(Collision collision)
    {
        var planet = collision.gameObject.GetComponent<Planet>();
        var asteroid = collision.gameObject.GetComponent<Asteroid>();

        if (planet == null && asteroid == null)
        {
            return;
        }
        
        if (planet != null)
        {
            GetComponent<Collider>().enabled = false;
            planet.GetComponent<Health>().RemoveFromCurrentHealth(transform.localScale.x / 100);

            FindObjectOfType<AsteroidManager>().DestroyAsteroid(asteroid);
        }

        if (asteroid != null)
        {
            //if (Random.Range(0, 2) == 0)
            //{
            //    FindObjectOfType<AsteroidManager>().CreateAsteroid(transform.position, transform.localScale.x + asteroid.transform.localScale.x);
            //}
            //else
            //{
            //
            //}
        }
    }
}