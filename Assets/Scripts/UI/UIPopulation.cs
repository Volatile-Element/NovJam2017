using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopulation : MonoBehaviour
{
    private Text txtPopulation;

    private void Awake()
    {
        txtPopulation = transform.Find("txtPopulation").GetComponent<Text>();
    }

	// Use this for initialization
	void Start ()
    {
        FindObjectOfType<Planet>().OnCurrentPopulationChanged.AddListener(OnCurrentPopulationChanged);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCurrentPopulationChanged(int currentPopulation)
    {
        txtPopulation.text = currentPopulation.ToString("N0");
    }
}