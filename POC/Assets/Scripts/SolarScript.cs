using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarScript : MonoBehaviour
{
    public GameObject ledPanel;
    private GameObject[] lights;
    private int noLights = 5;

    // Start is called before the first frame update
    void Start()
    {
        lights = new GameObject[ledPanel.transform.childCount];
        for(int i=0 ; i<ledPanel.transform.childCount ; i++)
        {
            lights[i] = ledPanel.transform.GetChild(i).gameObject;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void printActivate()
    {
        Debug.Log("activated");
        foreach (GameObject l in lights)
        {
            l.GetComponent<Light>().intensity = 0.3f;
        }
    }

    public void printDeActivate()
    {
        Debug.Log("De activated");
        foreach (GameObject l in lights)
        {
            l.GetComponent<Light>().intensity = 0f;
        }
    }
}
