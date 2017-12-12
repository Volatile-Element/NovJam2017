using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Canvas>().enabled = false;
    }

    public void Show(int lostPopulation, int savedPopulation)
    {
        GetComponent<Canvas>().enabled = true;

        var population = (100f / ((float)lostPopulation + (float)savedPopulation)) * (float)savedPopulation;

        transform.Find("Population Lost/txtPopulation").GetComponent<Text>().text = lostPopulation.ToString();
        transform.Find("Population Saved/txtPopulation").GetComponent<Text>().text = savedPopulation.ToString();
        transform.Find("txtPercentageSaved").GetComponent<Text>().text = $"You saved {population}%";
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }
}