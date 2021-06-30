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

    // private string Weight = null;

    void Start()
    {
        // Mass = this.name;
        splitArray = this.name.Split(char.Parse("_"));
        Mass = float.Parse(splitArray[1]);
        StartPos = this.transform.localPosition;

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
            // ObjectTouched.Find("text").gameObject.GetComponent<TextMeshPro>().text = splitArray[1] + " kg.";
            this.transform.localPosition = StartPos;
            Debug.Log(this.name + " --- " + this.transform.localPosition);
            // Rigidbody rigidbody = other.gameObject.GetComponent<Rigidbody>();
            // Debug.Log(rigidbody.mass);
            // rigidbody.mass = 2.1f;


            // Debug.Log("I'm Touch");
            // Debug.Log(rigidbody.mass);
        }
    }

    

    // IEnumerator Distance(Ray ray)
    // {
    //     RaycastHit hit;
        
    //     // Debug.DrawRay(transform.position, Vector3.right);

    //     if (Physics.Raycast(ray, out hit))
    //     {
    //         if (hit.collider.tag == "object")
    //         {
    //             // Debug.Log("Yes I'm Ray");
    //             string objname = hit.collider.name;
    //             Debug.Log(objname);
    //             objDistance = hit.collider.transform.position.x - ray.origin.x;
    //         }
    //     }
    //     if (rightbool)
    //     {
    //         leftDistance = objDistance;
    //         Debug.Log("Left distance =" +leftDistance);
    //         rightbool = false;
    //         StopCoroutine(Distance(leftRay));
    //     }
    //     else
    //     {
    //         rightDistance = objDistance;
    //         Debug.Log("Right distacne " +rightDistance);
    //         yield return new WaitForSeconds(0.1f);
    //         rightbool = true;
    //         StartCoroutine(Distance(leftRay));
    //     }

    // }

    // public void RotationofPlate()
    // {
    //     Weight = EventSystem.current.currentSelectedGameObject.name;
    //     string[] splitArray = Weight.Split(char.Parse("_"));

    //     if(splitArray[0] == "L")
    //     {
    //         leftMass = float.Parse(splitArray[1]);
    //         Debug.Log("leftMass" + leftMass);
    //     }
    //     else 
    //     {
    //         rightMass = float.Parse(splitArray[1]);
    //         Debug.Log("right Mass" + rightMass);
    //     }

    //     // if(Plate.transform.localEulerAngles.x < 90f)
    //     // {
    //     //     StartCoroutine(rotateObjectRight(Plate));
    //     // }
    //     // else if(Plate.transform.localEulerAngles.x > 90f)
    //     //     StartCoroutine(rotateObjectLeft(Plate));
    //     // if(rightMass * rightDistance > leftMass * leftDistance)
    //     // {

    //     // }
    //     if(rightMass*rightDistance == (-leftMass * leftDistance))
    //     {
    //            Debug.Log("equal");
    //     }
    //     else if(rightMass*rightDistance > (leftMass*leftDistance))
    //     {
            
    //         // Plate.transform.localEulerAngles = new Vector3(80f, 90f , 90f);
    //         StartCoroutine(rotateObjectRight(Plate));
    //     }
    //     else if(rightMass * rightDistance < (leftMass * leftDistance))
    //     {
    //             // Plate.transform.localEulerAngles = new Vector3(100f,90f, 90f);
    //         StartCoroutine(rotateObjectLeft(Plate));
    //     }
    // }

    // IEnumerator rotateObjectRight(GameObject go)
    // {
        
    //     for (int i = 0; i <= 20; i++)
    //     {

    //         // go.transform.localEulerAngles = new Vector3(go.transform.localEulerAngles.x + 0.5f, 90f, 90f);
    //         float rotationSpeed = 10f;

    //         go.transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
    //         yield return new WaitForSeconds(0.05f);
    //     }

    // }

    // IEnumerator rotateObjectLeft(GameObject go)
    // {
    //     for (int i = 0; i <= 20; i++)
    //     {
    //         // go.transform.localEulerAngles = new Vector3(go.transform.localEulerAngles.x - 0.5f, 90f, 90f);
    //         float rotationSpeed = -10f;

    //         go.transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
    //         yield return new WaitForSeconds(0.05f);
    //     }
    // }

    // public void ButtonAction()
    // {
    //     righRay = new Ray(transform.position, Vector3.right);
    //     leftRay = new Ray(transform.position, -Vector3.right);
    //     StartCoroutine(Distance(righRay));
    // }
}