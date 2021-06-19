using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MenuSceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static MenuSceneScript instance;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void logOut()
    {
        // FirebaseManager.instance.auth.SignOut();
        FirebaseManager.instance.auth.SignOut();
        GameManager.instance.ChangeScene(0);
    }
}
