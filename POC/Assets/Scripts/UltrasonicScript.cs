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
    public GameObject codebaseCanvas;

    void Start()
    {
        // codebaseCanvas = transform.Find("CodeBaseCanvas").gameObject;
        thresholdValue = transform.Find("CodeBaseCanvas").Find("ThresholdImage").Find("ThresholdValue").gameObject;
        thresholdText = transform.Find("CodeBaseCanvas").Find("ThresholdImage").Find("ThresholdText").gameObject;
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
        StartCoroutine(removeCodebase());
    }

    IEnumerator removeCodebase()
    {
        yield return new WaitForSeconds(1.5f);

        codebaseCanvas.SetActive(false);
    }
}