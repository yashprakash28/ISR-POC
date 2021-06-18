using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TrundleWheel : MonoBehaviour
{
    private GameObject wheel;
    private float curPosX, lastPosX;
    private TextMeshPro currentDistance;
    private TextMeshPro angleRotated;
    private float circumference = 100f;
    private float singleRotationAngle = 2f * Mathf.PI;
    private float radius = 0f;
    public Slider sliderCircum;

    public TextMeshPro circumferenceTextUI;

    public TextMeshPro distanceCalculation;
    public TextMeshProUGUI cText;

    private float unityScale = 14f; // -7 to +7
    private float totalAngleRotated = 0f; // in degrees. distance will be calculated accordingly
    private float anglePerUnitDistance = 0f;

    public Image fillImage;
    public Image DisFill;

    private float initialX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        anglePerUnitDistance = totalAngleRotated / unityScale;

        initialX = transform.position.x;
        Debug.Log(initialX);

        wheel = transform.Find("BottomAnchor").Find("Wheel").gameObject;
        currentDistance = transform.Find("CurrentDistance").gameObject.GetComponent<TextMeshPro>();
        angleRotated = transform.Find("AngleRotated").gameObject.GetComponent<TextMeshPro>();

        radius = roundto2Digits(circumference / singleRotationAngle);
        totalAngleRotated = circumference / radius;
        circumferenceTextUI.text = "Radius(r) = Circumference(c) / 2pi\n" +
                            radius.ToString() + " cm = " + circumference.ToString() + " cm / 2pi";
    }

    // Update is called once per frame
    void Update()
    {
        // wheel.transform.Rotate(new Vector3(0f, 0f, -100f));
        // Debug.Log(wheel.transform.localRotation.z);
        // transform.RotateAround (transform.position, transform.up, 90)
        // Debug.Log(wheel.transform.rotation);
        Debug.Log(wheel.transform.eulerAngles);
        curPosX = transform.position.x;
        curPosX = roundto2Digits(curPosX);
        if (curPosX == lastPosX)
        {
            Debug.Log("Not moving");
            // Debug.Log((curPosX-lastPosX)*51.22f);
        }
        else
        {
            updateVariables();
        }
        lastPosX = curPosX;
    }

    private float roundto2Digits(float x)
    {
        x = (Mathf.Round(x * 100f)) / 100f;
        return x;
    }

    private float convertToRadian(float x)
    {
        x = (x / 360f) * 2f * Mathf.PI;
        return x;
    }

    private float convertToDegree(float x)
    {
        x /= 2;
        x /= Mathf.PI;
        x *= 360f;
        return x;
    }

    public void CircumSlider()
    {

        // Debug.Log(sliderCircum.value);
        scaleWheel(sliderCircum.value / 100f);
        circumference = sliderCircum.value;
        radius = roundto2Digits(circumference / singleRotationAngle);

        circumferenceTextUI.text = "Radius(r) = Circumference(c) / 2pi\n" +
                            radius.ToString() + " cm = " + circumference.ToString() + " cm / 2pi";

        cText.text = circumference.ToString() + " cm";

        // totalAngleRotated = circumference / radius;
        // Debug.Log(circumference + " " + radius + " " + (totalAngleRotated*360)/(singleRotationAngle));

        updateVariables();
    }

    private void scaleWheel(float scale)
    {
        transform.Find("BottomAnchor").localScale = new Vector3(1f * scale, 1f * scale, 1f * scale);
        Debug.Log(transform.Find("BottomAnchor").localScale);
    }

    public void updateVariables()
    {
        float distance = roundto2Digits(((initialX - curPosX) * circumference) / 14f);
        float angleRadians = distance / radius;
        float angleDegree = convertToDegree(angleRadians);

        wheel.transform.eulerAngles = new Vector3(0f, 0f, angleDegree);
        fillImage.fillAmount = angleDegree/360f;
        DisFill.fillAmount = distance/circumference;


        currentDistance.text = "d = " + distance.ToString() + " cm";
        angleRotated.text = "a = " + angleDegree.ToString() + "<sup>o</sup>";

        distanceCalculation.text = "Distance (d) = radius (r) x angle (a)\n"
                                    + radius.ToString() + " cm x "
                                    + roundto2Digits(angleRadians).ToString()
                                    + " = " + distance.ToString()
                                    + " cm \n Here angle is in radian(s)";
    }

    private void fillCircle(float fillValue)
    {
        fillImage.fillAmount = fillValue;
    }
}