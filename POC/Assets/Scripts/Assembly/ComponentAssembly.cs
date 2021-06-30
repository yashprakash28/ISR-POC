using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComponentAssembly : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject AssemblyTable;
    // [SerializeField]
    private GameObject Obj;

    [SerializeField]
    private GameObject Button;
    private bool ex;
    private int count = 0;


    void Start()
    {
        // _assemblyDataInJson = JsonUtility.FromJson<AssemblyData>(jsonFile.text);
        // Obj = 
        Debug.Log(SceneManager.GetActiveScene().name);
        Obj = AssemblyTable.transform.Find("AssemblingArea").Find(SceneManager.GetActiveScene().name).gameObject;
        Debug.Log(Obj.name);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Component")
        {
            Debug.Log("chal raha hai");
            collision.gameObject.SetActive(false);
            Obj.transform.Find(collision.gameObject.name).transform.gameObject.SetActive(true);
            count++;
            Debug.Log(count);
            // if (collision.gameObject.name == "Part_1")
            // {
            //     ex = true;
            //     Debug.Log(collision.gameObject.name);
            //     collision.gameObject.SetActive(false);
            //     SolarLight.transform.Find(collision.gameObject.name).transform.gameObject.SetActive(true);
            //     count++;
            // }
            // if(ex && collision.contactCount > 1)
            // {
            //     Debug.Log("Collision Occur here");
            //     collision.gameObject.SetActive(false);
            //     SolarLight.transform.Find(collision.gameObject.name).transform.gameObject.SetActive(true);
            //     count++;
            // }
            if (count == Obj.transform.childCount)
            {
                Debug.Log("ChildCountComplete");
                Button.SetActive(true);
            }

        }
    }
}
