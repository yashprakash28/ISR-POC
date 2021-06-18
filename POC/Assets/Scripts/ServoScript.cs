using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServoScript : MonoBehaviour
{
    private float fanSpeed;
    private GameObject wing;

    // Start is called before the first frame update
    void Start()
    {
        wing = GameObject.Find("Wing").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        fanSpeed = Mathf.Pow( (100f - UltrasonicScript.distance)/10f, 2f ); // taking speed reference from ultrasonic
        // Debug.Log("fanSpeed: " + fanSpeed);

        if (UltrasonicScript.distance <= UltrasonicScript.thresholdDistance)
        {
            wing.transform.Rotate(Vector3.forward * 100f * Time.deltaTime);
        }
        else
        {
            wing.transform.Rotate(Vector3.forward * 0f * Time.deltaTime);
        }
    }
}