using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInteraction : MonoBehaviour
{
    public void Show(string text)
    {
        GetComponent<Canvas>().enabled = true;
        transform.Find("txtInteract").GetComponent<Text>().text = text;
    }

    public void Hide()
    {
        GetComponent<Canvas>().enabled = false;
    }
}