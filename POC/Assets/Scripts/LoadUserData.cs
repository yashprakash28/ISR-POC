using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadUserData : MonoBehaviour
{
    [SerializeField]    public  TextMeshProUGUI _username;
    [SerializeField]    public TextMeshProUGUI _email;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FetchData());
        
    }

    IEnumerator FetchData()
    {
        yield return StartCoroutine(FirebaseManager.instance.LoadUserData());
        
        Debug.Log("from data manager: " + FirebaseManager.instance._usernametext);
        Debug.Log("from data manager: " + FirebaseManager.instance._emailtext);        
        _username.text = FirebaseManager.instance._usernametext;
        _email.text = FirebaseManager.instance._emailtext;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
