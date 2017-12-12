using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Time.timeScale = 1;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Time.timeScale = 3;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Time.timeScale = 7;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            FindObjectOfType<PlanetDeath>().OnDeath();
        }
	}
}