using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using UnityEngine.SceneManagement;

    public class SceneManagment: MonoBehaviour 
    {  
        
        public void SceneManage(string SceneName) 
        {  
            
            SceneManager.LoadScene(SceneName);  
            // DontDestroyOnLoad(SceneName);
        }    
    }   
