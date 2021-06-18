using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Dynamic;
// using UnityEngine.Json.NET;

public class ReadQuizJson : MonoBehaviour
{
    public TextAsset jsonFile;

    [Serializable]
    public class quizData
    {
        //these variables are case sensitive and must match the strings "firstName" and "lastName" in the JSON.
        // public string problem_set;
        public string question;
        public string option_1;
        public string option_2;
        public string option_3;
        public string option_4;
        public string correct;
    }

    [Serializable]
    public class quizDatas
    {
        public quizData[] seeSaw;
        // public quizData[] rpi;
    }
    
    quizDatas quizDatasInJson;
    void Start()
    {
        quizDatasInJson = JsonUtility.FromJson<quizDatas>(jsonFile.text);
        // string componentName = "arduino";
        // Type type = Type.GetType(String.Format("quizDatasInJson.{0}", componentName));
        // Debug.Log("sfvfs " + type);
        
    }

    public void getQuestions()
    {
        // ToDo: pass string of component
        // string componentName = "arduino";
        // string temp = String.Format("quizDatasInJson.{0}", componentName);  // temp = quizDatasInJson.arduino

        // dynamic expando = new ExpandoObject();
        // var map = expando as IDictionary<string, quizDatas>;
        // map.Add(temp, quizDatas.arduino);
        // Type type = Type.GetType(String.Format("quizDatasInJson.{0}", componentName));

        foreach (quizData quizData in quizDatasInJson.seeSaw)
        {
            Debug.Log(  quizData.question + " " + 
                        quizData.option_1 + " " +
                        quizData.option_2 + " " +
                        quizData.option_3 + " " +
                        quizData.option_4 + " " +
                        quizData.correct  );

            
        }
    }

    // public void showSetRpi(string set)
    // {
    //     // ToDo: pass string of component

    //     foreach (quizData quizData in quizDatasInJson.rpi)
    //     {
    //         if(quizData.problem_set == set)
    //         {
    //             Debug.Log(  quizData.question + " " + 
    //                         quizData.option_1 + " " +
    //                         quizData.option_2 + " " +
    //                         quizData.option_3 + " " +
    //                         quizData.option_4 + " " +
    //                         quizData.correct  );
    //         }            
    //     }
    // }

}

