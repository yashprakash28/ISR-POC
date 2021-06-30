using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class SeeSawScript : MonoBehaviour
{
    // Start is called before the first frame update
    // public float hight;
    // bool rightbool = false;
    // public GameObject Plate;
    // float rightMass = 10f ,leftMass = 10f;
    // float objDistance, rightDistance = 1f ,leftDistance = 1f;
    // Ray righRay, leftRay;

    private float Mass = 0.0f;
    private string[] splitArray;
    private Vector3 StartPos;
    private Vector3 startRot;

    // private string Weight = null;

    void Start()
    {
        // Mass = this.name;
        splitArray = this.name.Split(char.Parse("_"));
        Mass = float.Parse(splitArray[1]);
        StartPos = this.transform.localPosition;
        startRot = this.transform.localEulerAngles;

        Debug.Log(this.name + " --- " + StartPos);

    }

    // Update is called once per frame
    void Update()
    {
        // Weight = EventSystem.current.currentSelectedGameObject.name;
        // if (Weight != null)
        // {
        //     RotationofPlate(Weight);
        // }
    }

    private void OnCollisionEnter(Collision other)
    {
        // Debug.Log("I'm Touch");
        if (other.gameObject.tag == "object")
        {
            GameObject ObjectTouched = other.gameObject;
            ObjectTouched.GetComponent<Rigidbody>().mass = (Mass/10);
            ObjectTouched.transform.GetChild(0).GetComponent<TextMeshPro>().text = splitArray[1] + " kg.";
            
            this.transform.localPosition = StartPos;
            this.transform.localEulerAngles = startRot;
            // StartCoroutine((reInit()));

            Debug.Log(this.name + " --- " + this.transform.localPosition);

        }
    }

    // IEnumerator reInit()
    // {
    //     yield return new WaitForSeconds(1.5f);

        
    // }

}