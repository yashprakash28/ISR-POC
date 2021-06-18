using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEDScript : MonoBehaviour
{
    private float intensityFactor;
    private GameObject light;
    
    // Start is called before the first frame update
    void Start()
    {
        light = GameObject.Find("Point Light").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        intensityFactor = Mathf.Pow( ((100f - UltrasonicScript.distance) / 10f), 2f);
        // Debug.Log("Intensity: " + intensityFactor);

        if (UltrasonicScript.distance <= UltrasonicScript.thresholdDistance)
        {
            light.GetComponent<Light>().intensity = intensityFactor;
        }
        else
        {
            light.GetComponent<Light>().intensity = 0f;
        }
    }
}
