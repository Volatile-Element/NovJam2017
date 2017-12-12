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
        GetComponent<Health>().SetMaxHealth(100f, true);

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

    public void KillRemainingPop()
    {
        LostPopulation += CurrentPopulation;
        CurrentPopulation = 0;
    }

    public void KillOffPopulationPercentage(float percetnage)
    {
        var lostPopulation = CurrentPopulation * (1 - (percetnage / 100));

        CurrentPopulation -= (int)lostPopulation;
        LostPopulation += (int)lostPopulation;

        OnCurrentPopulationChanged.Invoke(CurrentPopulation);
    }

    private IEnumerator FleeThePlanet()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0f, 2f));

            var escapingAmount = Random.Range(100000, 1000000);

            CurrentPopulation -= escapingAmount;
            EvacuatedPopulation += escapingAmount;

            OnCurrentPopulationChanged.Invoke(CurrentPopulation);
        }
    }
}