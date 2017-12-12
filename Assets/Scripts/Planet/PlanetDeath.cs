using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetDeath : MonoBehaviour
{
    public GameObject[] Asteroids;

    public AudioSource AudioSource;

    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
        GetComponent<Death>().OnDeath.AddListener(OnDeath);
    }

	public void OnDeath()
    {
        StartCoroutine(DoActionIn(() =>
        {
            FindObjectOfType<FollowCameraWithAnchor>().SetInterests(transform, transform.Find("Camera Anchor"));
            AudioSource.Play();
        }, 0f));

        StartCoroutine(DoActionIn(() =>
        {
            StartCoroutine(MoveTarget());
        }, 2f));

        StartCoroutine(DoActionIn(() =>
        {
            //Hide Planet Model
            transform.Find("Model").gameObject.SetActive(false);

            //Spawn Asteroids
            for (int i = 0; i < 25; i++)
            {
                var asteroid = Instantiate(Asteroids[UnityEngine.Random.Range(0, Asteroids.Length)], transform.position + new Vector3(UnityEngine.Random.Range(-100, 100), UnityEngine.Random.Range(-100, 100), UnityEngine.Random.Range(-100, 100)), Quaternion.identity);
                var scale = UnityEngine.Random.Range(1, 10);
                asteroid.transform.localScale = Vector3.one * scale;
                var rb = asteroid.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(new Vector3(UnityEngine.Random.Range(10, 100), UnityEngine.Random.Range(10, 100), UnityEngine.Random.Range(10, 100)));
                }
            }

            //Particle Dust
        }, 6.5f));

        StartCoroutine(DoActionIn(() =>
        {
            //Pan Up
        }, 0f));

        StartCoroutine(DoActionIn(() =>
        {
            var planet = GetComponent<Planet>();
            planet.KillRemainingPop();

            FindObjectOfType<UIGameOver>().Show(planet.LostPopulation, planet.EvacuatedPopulation);
        }, 15f));
    }

    private IEnumerator DoActionIn(Action action, float secondsUntilExecution)
    {
        yield return new WaitForSeconds(secondsUntilExecution);

        action.Invoke();
    }

    private IEnumerator MoveTarget()
    {
        while (true)
        {
            transform.Find("Camera Anchor").position += new Vector3(UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(-10, 10));

            yield return new WaitForSeconds(0.1f);
        }
    }
}