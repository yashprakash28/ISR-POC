using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UltrasonicScript : MonoBehaviour
{
    public Camera camera;
    public static float distance;
    public static float thresholdDistance;
    private GameObject thresholdValue;
    private GameObject thresholdText;

    void Start()
    {
        thresholdValue = GameObject.Find("Canvas").transform.Find("ThresholdImage").Find("ThresholdValue").gameObject;
        thresholdText = GameObject.Find("Canvas").transform.Find("ThresholdImage").Find("ThresholdText").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(this.transform.position, camera.transform.position);
        Debug.Log(distance);
    }

    public void saveButton()
    {
        string thresholdValueInput = thresholdValue.GetComponent<TMP_InputField>().text;
        thresholdDistance = float.Parse(thresholdValueInput);
        thresholdText.GetComponent<TextMeshProUGUI>().text = "Enter Threshold Distance for Sensor\nCurrent Value: " + thresholdValueInput + " units";
    }
}