using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class quiz : MonoBehaviour
{

    private int CurrentQuestionIndex = 0;
    public GameObject quizCanvas;
    private GameObject correctAnsAudio;
    public bool imageQuestion = false;

    public string topic;

    public TextAsset jsonFile;

/// <summary>this class is made to access data from json file</summary>
    [Serializable]
    public class quizData
    {
        //these variables are case sensitive and must match the strings "firstName" and "lastName" in the JSON.
        public string question;
        public string option_1;
        public string option_2;
        public string option_3;
        public string option_4;
        public int correct;
    }

    [Serializable]
    public class quizDatas
    {
        public quizData[] seeSaw;
        public quizData[] trundlewheel;
        public quizData[] solarlight;
    }

    quizDatas quizDatasInJson;

    void Start()
    {
        // initialize json file and start reading it
        quizDatasInJson = JsonUtility.FromJson<quizDatas>(jsonFile.text);
        getQuestions();

        correctAnsAudio = quizCanvas.transform.Find("CorrectAnsAudio").gameObject;

        quizCanvas.GetComponent<AudioSource>().enabled = false;

        switch (topic)
        {
            case "seesaw": quizCanvas.transform.Find("ParentPanel").Find("QuestionDisplay").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.seeSaw[CurrentQuestionIndex].question;

                            quizCanvas.transform.Find("ParentPanel").Find("1").Find("OptionText1").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.seeSaw[CurrentQuestionIndex].option_1;
                            quizCanvas.transform.Find("ParentPanel").Find("2").Find("OptionText2").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.seeSaw[CurrentQuestionIndex].option_2;
                            quizCanvas.transform.Find("ParentPanel").Find("3").Find("OptionText3").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.seeSaw[CurrentQuestionIndex].option_3;
                            quizCanvas.transform.Find("ParentPanel").Find("4").Find("OptionText4").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.seeSaw[CurrentQuestionIndex].option_4;

                            break;
            
            case "trundlewheel": quizCanvas.transform.Find("ParentPanel").Find("QuestionDisplay").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.trundlewheel[CurrentQuestionIndex].question;

                            quizCanvas.transform.Find("ParentPanel").Find("1").Find("OptionText1").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.trundlewheel[CurrentQuestionIndex].option_1;
                            quizCanvas.transform.Find("ParentPanel").Find("2").Find("OptionText2").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.trundlewheel[CurrentQuestionIndex].option_2;
                            quizCanvas.transform.Find("ParentPanel").Find("3").Find("OptionText3").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.trundlewheel[CurrentQuestionIndex].option_3;
                            quizCanvas.transform.Find("ParentPanel").Find("4").Find("OptionText4").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.trundlewheel[CurrentQuestionIndex].option_4;

                            break;

            case "solarlight" : quizCanvas.transform.Find("ParentPanel").Find("QuestionDisplay").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.solarlight[CurrentQuestionIndex].question;

                            quizCanvas.transform.Find("ParentPanel").Find("1").Find("OptionText1").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.solarlight[CurrentQuestionIndex].option_1;
                            quizCanvas.transform.Find("ParentPanel").Find("2").Find("OptionText2").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.solarlight[CurrentQuestionIndex].option_2;
                            quizCanvas.transform.Find("ParentPanel").Find("3").Find("OptionText3").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.solarlight[CurrentQuestionIndex].option_3;
                            quizCanvas.transform.Find("ParentPanel").Find("4").Find("OptionText4").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.solarlight[CurrentQuestionIndex].option_4;

                            break;
            
            default: Debug.Log("no data");
                    break;
        }
    }

    // Utility function to check functioning of json file
    public void getQuestions()
    {
        switch (topic)
        {
            case "seesaw" : foreach (quizData quizData in quizDatasInJson.seeSaw)
                            {
                                Debug.Log(  quizData.question + " " + 
                                            quizData.option_1 + " " +
                                            quizData.option_2 + " " +
                                            quizData.option_3 + " " +
                                            quizData.option_4 + " " +
                                            quizData.correct  ); 
                            }
                            break;
            
            case "trundlewheel" : foreach (quizData quizData in quizDatasInJson.trundlewheel)
                                    {
                                        Debug.Log(  quizData.question + " " + 
                                                    quizData.option_1 + " " +
                                                    quizData.option_2 + " " +
                                                    quizData.option_3 + " " +
                                                    quizData.option_4 + " " +
                                                    quizData.correct  );
                                    }
                                    break;

            case "solarlight" : foreach (quizData quizData in quizDatasInJson.solarlight)
                                    {
                                        Debug.Log(  quizData.question + " " + 
                                                    quizData.option_1 + " " +
                                                    quizData.option_2 + " " +
                                                    quizData.option_3 + " " +
                                                    quizData.option_4 + " " +
                                                    quizData.correct  );
                                    }
                                    break;

            default: Debug.Log("no data");
                    break;
        }
    }

    public void CheckAnswer()
    {
        string ButtonClickedName = EventSystem.current.currentSelectedGameObject.name;

        switch (topic)
        {
            case "seesaw" : if(ButtonClickedName == quizDatasInJson.seeSaw[CurrentQuestionIndex].correct.ToString()) 
                            {
                                StartCoroutine(ShowNextQuestion());
                            }
                            else
                            {
                                StartCoroutine(ShowRedAlert());
                            }
                            break;
            
            case "trundlewheel" :   if(ButtonClickedName == quizDatasInJson.trundlewheel[CurrentQuestionIndex].correct.ToString()) 
                                    {
                                        StartCoroutine(ShowNextQuestion());
                                    }
                                    else
                                    {
                                        StartCoroutine(ShowRedAlert());
                                    }
                                    break;

            case "solarlight" : if(ButtonClickedName == quizDatasInJson.solarlight[CurrentQuestionIndex].correct.ToString()) 
                            {
                                StartCoroutine(ShowNextQuestion());
                            }
                            else
                            {
                                StartCoroutine(ShowRedAlert());
                            }
                            break;

            default: Debug.Log("no data");
                    break;
        }
    }

    IEnumerator ShowNextQuestion()
    {
        Image ButtonColor = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
        ButtonColor.color = new Color32(0, 128, 0, 130);        // green Color
        
        //AlertScreen.SetActive(true);
        correctAnsAudio.GetComponent<AudioSource>().enabled = true;
        correctAnsAudio.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(1.0f);
        ButtonColor.color = new Color32(14, 23, 130, 130);       // Revert color back to original color

        CurrentQuestionIndex++;

        switch (topic)
        {
            case "seesaw" : if(CurrentQuestionIndex < quizDatasInJson.seeSaw.Length)
                            {
                                // Image optionButton (if the user has a question with options as images)
                                if (imageQuestion == true)
                                {
                                    // run image if-else block
                                    if(CurrentQuestionIndex == quizDatasInJson.seeSaw.Length-1)
                                    {
                                        // Update Question
                                        quizCanvas.transform.Find("ParentPanel").Find("QuestionDisplay").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.seeSaw[CurrentQuestionIndex].question;

                                        // Update all the options as null text
                                        for(int i=1;i<=4;i++)
                                        {
                                            quizCanvas.transform.Find("ParentPanel").Find(i.ToString()).Find("OptionText" + i.ToString()).GetComponent<TextMeshProUGUI>().text = "";
                                        }

                                        // display images
                                        Debug.Log("Images");
                                        quizCanvas.transform.Find("ParentPanel").Find("1").GetComponent<Image>().sprite = Resources.Load<Sprite>("CircuitImages/" + quizDatasInJson.seeSaw[CurrentQuestionIndex].option_1);
                                        quizCanvas.transform.Find("ParentPanel").Find("2").GetComponent<Image>().sprite = Resources.Load<Sprite>("CircuitImages/" + quizDatasInJson.seeSaw[CurrentQuestionIndex].option_2);
                                        quizCanvas.transform.Find("ParentPanel").Find("3").GetComponent<Image>().sprite = Resources.Load<Sprite>("CircuitImages/" + quizDatasInJson.seeSaw[CurrentQuestionIndex].option_3);
                                        quizCanvas.transform.Find("ParentPanel").Find("4").GetComponent<Image>().sprite = Resources.Load<Sprite>("CircuitImages/" + quizDatasInJson.seeSaw[CurrentQuestionIndex].option_4);
                                    }

                                    else
                                    {
                                        quizCanvas.transform.Find("ParentPanel").Find("QuestionDisplay").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.seeSaw[CurrentQuestionIndex].question;

                                        // display text options
                                        quizCanvas.transform.Find("ParentPanel").Find("1").Find("OptionText1").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.seeSaw[CurrentQuestionIndex].option_1;
                                        quizCanvas.transform.Find("ParentPanel").Find("2").Find("OptionText2").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.seeSaw[CurrentQuestionIndex].option_2;
                                        quizCanvas.transform.Find("ParentPanel").Find("3").Find("OptionText3").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.seeSaw[CurrentQuestionIndex].option_3;
                                        quizCanvas.transform.Find("ParentPanel").Find("4").Find("OptionText4").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.seeSaw[CurrentQuestionIndex].option_4;

                                    } 
                                }

                                // if there is no image as an option // run simple block
                                else
                                {
                                    quizCanvas.transform.Find("ParentPanel").Find("QuestionDisplay").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.seeSaw[CurrentQuestionIndex].question;

                                    // display text options
                                    quizCanvas.transform.Find("ParentPanel").Find("1").Find("OptionText1").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.seeSaw[CurrentQuestionIndex].option_1;
                                    quizCanvas.transform.Find("ParentPanel").Find("2").Find("OptionText2").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.seeSaw[CurrentQuestionIndex].option_2;
                                    quizCanvas.transform.Find("ParentPanel").Find("3").Find("OptionText3").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.seeSaw[CurrentQuestionIndex].option_3;
                                    quizCanvas.transform.Find("ParentPanel").Find("4").Find("OptionText4").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.seeSaw[CurrentQuestionIndex].option_4;
                                }
                            }
                            else
                            {
                                StartCoroutine(proceedTextDisplay());
                            }
                            break;
            
            case "trundlewheel" :   if(CurrentQuestionIndex < quizDatasInJson.trundlewheel.Length)
                                    {
                                        // Image optionButton (if the user has a question with options as images)
                                        if (imageQuestion == true)
                                        {
                                            // run image if-else block
                                            if(CurrentQuestionIndex == quizDatasInJson.trundlewheel.Length-1)
                                            {
                                                // Update Question
                                                quizCanvas.transform.Find("ParentPanel").Find("QuestionDisplay").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.trundlewheel[CurrentQuestionIndex].question;

                                                // Update all the options as null text
                                                for(int i=1;i<=4;i++)
                                                {
                                                    quizCanvas.transform.Find("ParentPanel").Find(i.ToString()).Find("OptionText" + i.ToString()).GetComponent<TextMeshProUGUI>().text = "";
                                                }

                                                // display images
                                                Debug.Log("Images");
                                                quizCanvas.transform.Find("ParentPanel").Find("1").GetComponent<Image>().sprite = Resources.Load<Sprite>("CircuitImages/" + quizDatasInJson.trundlewheel[CurrentQuestionIndex].option_1);
                                                quizCanvas.transform.Find("ParentPanel").Find("2").GetComponent<Image>().sprite = Resources.Load<Sprite>("CircuitImages/" + quizDatasInJson.trundlewheel[CurrentQuestionIndex].option_2);
                                                quizCanvas.transform.Find("ParentPanel").Find("3").GetComponent<Image>().sprite = Resources.Load<Sprite>("CircuitImages/" + quizDatasInJson.trundlewheel[CurrentQuestionIndex].option_3);
                                                quizCanvas.transform.Find("ParentPanel").Find("4").GetComponent<Image>().sprite = Resources.Load<Sprite>("CircuitImages/" + quizDatasInJson.trundlewheel[CurrentQuestionIndex].option_4);
                                            }

                                            else
                                            {
                                                quizCanvas.transform.Find("ParentPanel").Find("QuestionDisplay").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.trundlewheel[CurrentQuestionIndex].question;

                                                // display text options
                                                quizCanvas.transform.Find("ParentPanel").Find("1").Find("OptionText1").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.trundlewheel[CurrentQuestionIndex].option_1;
                                                quizCanvas.transform.Find("ParentPanel").Find("2").Find("OptionText2").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.trundlewheel[CurrentQuestionIndex].option_2;
                                                quizCanvas.transform.Find("ParentPanel").Find("3").Find("OptionText3").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.trundlewheel[CurrentQuestionIndex].option_3;
                                                quizCanvas.transform.Find("ParentPanel").Find("4").Find("OptionText4").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.trundlewheel[CurrentQuestionIndex].option_4;

                                            } 
                                        }

                                        // if there is no image as an option // run simple block
                                        else
                                        {
                                            quizCanvas.transform.Find("ParentPanel").Find("QuestionDisplay").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.trundlewheel[CurrentQuestionIndex].question;

                                            // display text options
                                            quizCanvas.transform.Find("ParentPanel").Find("1").Find("OptionText1").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.trundlewheel[CurrentQuestionIndex].option_1;
                                            quizCanvas.transform.Find("ParentPanel").Find("2").Find("OptionText2").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.trundlewheel[CurrentQuestionIndex].option_2;
                                            quizCanvas.transform.Find("ParentPanel").Find("3").Find("OptionText3").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.trundlewheel[CurrentQuestionIndex].option_3;
                                            quizCanvas.transform.Find("ParentPanel").Find("4").Find("OptionText4").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.trundlewheel[CurrentQuestionIndex].option_4;
                                        }
                                    }
                                    else
                                    {
                                        StartCoroutine(proceedTextDisplay());
                                    }
                                    break;
            
            case "solarlight" : if(CurrentQuestionIndex < quizDatasInJson.solarlight.Length)
                            {
                                // Image optionButton (if the user has a question with options as images)
                                if (imageQuestion == true)
                                {
                                    // run image if-else block
                                    if(CurrentQuestionIndex == quizDatasInJson.solarlight.Length-1)
                                    {
                                        // Update Question
                                        quizCanvas.transform.Find("ParentPanel").Find("QuestionDisplay").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.solarlight[CurrentQuestionIndex].question;

                                        // Update all the options as null text
                                        for(int i=1;i<=4;i++)
                                        {
                                            quizCanvas.transform.Find("ParentPanel").Find(i.ToString()).Find("OptionText" + i.ToString()).GetComponent<TextMeshProUGUI>().text = "";
                                        }

                                        // display images
                                        Debug.Log("Images");
                                        quizCanvas.transform.Find("ParentPanel").Find("1").GetComponent<Image>().sprite = Resources.Load<Sprite>("CircuitImages/" + quizDatasInJson.solarlight[CurrentQuestionIndex].option_1);
                                        quizCanvas.transform.Find("ParentPanel").Find("2").GetComponent<Image>().sprite = Resources.Load<Sprite>("CircuitImages/" + quizDatasInJson.solarlight[CurrentQuestionIndex].option_2);
                                        quizCanvas.transform.Find("ParentPanel").Find("3").GetComponent<Image>().sprite = Resources.Load<Sprite>("CircuitImages/" + quizDatasInJson.solarlight[CurrentQuestionIndex].option_3);
                                        quizCanvas.transform.Find("ParentPanel").Find("4").GetComponent<Image>().sprite = Resources.Load<Sprite>("CircuitImages/" + quizDatasInJson.solarlight[CurrentQuestionIndex].option_4);
                                    }

                                    else
                                    {
                                        quizCanvas.transform.Find("ParentPanel").Find("QuestionDisplay").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.seeSaw[CurrentQuestionIndex].question;

                                        // display text options
                                        quizCanvas.transform.Find("ParentPanel").Find("1").Find("OptionText1").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.solarlight[CurrentQuestionIndex].option_1;
                                        quizCanvas.transform.Find("ParentPanel").Find("2").Find("OptionText2").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.solarlight[CurrentQuestionIndex].option_2;
                                        quizCanvas.transform.Find("ParentPanel").Find("3").Find("OptionText3").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.solarlight[CurrentQuestionIndex].option_3;
                                        quizCanvas.transform.Find("ParentPanel").Find("4").Find("OptionText4").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.solarlight[CurrentQuestionIndex].option_4;

                                    } 
                                }

                                // if there is no image as an option // run simple block
                                else
                                {
                                    quizCanvas.transform.Find("ParentPanel").Find("QuestionDisplay").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.solarlight[CurrentQuestionIndex].question;

                                    // display text options
                                    quizCanvas.transform.Find("ParentPanel").Find("1").Find("OptionText1").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.solarlight[CurrentQuestionIndex].option_1;
                                    quizCanvas.transform.Find("ParentPanel").Find("2").Find("OptionText2").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.solarlight[CurrentQuestionIndex].option_2;
                                    quizCanvas.transform.Find("ParentPanel").Find("3").Find("OptionText3").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.solarlight[CurrentQuestionIndex].option_3;
                                    quizCanvas.transform.Find("ParentPanel").Find("4").Find("OptionText4").GetComponent<TextMeshProUGUI>().text = quizDatasInJson.solarlight[CurrentQuestionIndex].option_4;
                                }
                            }
                            else
                            {
                                StartCoroutine(proceedTextDisplay());
                            }
                            break;
            
            default: Debug.Log("no data");
                    break;
        }

    }

    IEnumerator proceedTextDisplay()
    {
        quizCanvas.transform.Find("ParentPanel").Find("proceedText").gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        quizCanvas.SetActive(false);
    }

    IEnumerator ShowRedAlert()
    {
        Image ButtonColor = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
        ButtonColor.color = new Color32(255, 0, 0, 130);        // Red Color

        quizCanvas.GetComponent<AudioSource>().enabled = true;
        quizCanvas.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1.0f);
        ButtonColor.color = new Color32(14, 23, 130, 130);       // Revert color back to white
    }
}
