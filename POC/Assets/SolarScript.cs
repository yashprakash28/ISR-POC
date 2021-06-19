using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarScript : MonoBehaviour
{
    public GameObject ledPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // distance = Vector3.Distance(solarPanel.transform.position, leapHand.transform.position);
        // Debug.Log(distance);
        // Debug.Log(leapHand.transform.position);
    }

    public void printActivate()
    {
        Debug.Log("activated");
    }

    public void printDeActivate()
    {
        Debug.Log("De activated");
    }
}
