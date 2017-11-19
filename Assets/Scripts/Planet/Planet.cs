using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public UnityEventFor<int> OnCurrentPopulationChanged = new UnityEventFor<int>();
    public UnityEventFor<int> OnEvacuatedPopulationChanged = new UnityEventFor<int>();
    public UnityEventFor<int> OnLostPopulationChanged = new UnityEventFor<int>();

    private int StartingPopulation;
    public int CurrentPopulation;

    public int EvacuatedPopulation;
    public int LostPopulation;
    
    private void Awake()
    {
        GetComponent<SphereCollider>().radius = transform.Find("Model").localScale.x / 2;
        GetComponent<Death>().OnDeath.AddListener(OnDeath);
    }

    private void Start()
    {
        StartCoroutine(FleeThePlanet());
    }

    public void OnDeath()
    {
        //Play death animation.
        //Do a thing.
    }

    private IEnumerator FleeThePlanet()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0f, 2f));

            CurrentPopulation -= Random.Range(100000, 1000000);
            OnCurrentPopulationChanged.Invoke(CurrentPopulation);
        }
    }
}