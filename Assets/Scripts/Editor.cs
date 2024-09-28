using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Editor : MonoBehaviour
{
    List<Question> qs;

    TMP_InputField qtext;
    TMP_InputField atext;

    Data data;
    int index;
    string lastQText = "";
    
    // Start is called before the first frame update
    void Start()
    {
        //transform.AddComponent<Data>();
        data = transform.GetComponent<Data>();
        qs = data.GetQuestions();
        index = qs.Count;
        qtext = transform.parent.GetChild(0).GetComponent<TMP_InputField>();
        atext = transform.parent.GetChild(1).GetComponent<TMP_InputField>();
        ViewNextQ();
    }

    public void AddQ()
    {
        data.AddQuestion(qtext.text, atext.text);
        index = qs.Count;
        ViewNextQ();
    }

    public void EditQ()
    {
        data.EditQuestion(lastQText, qtext.text, atext.text);
        index = qs.Count;
        ViewNextQ();
    }

    public void DeleteQ()
    {
        data.EditQuestion(qtext.text);
        ViewNextQ();

        if (qs.Count == 1) 
        {
            qtext.text = "";
            atext.text = "";
        }
    }

    public void ViewNextQ()
    {
        index--;
        qtext.text = qs[index].qText;
        atext.text = qs[index].aText;
        lastQText = qtext.text;
        if (index == 0)
        {
            index = qs.Count;
        }
    }

    public static void returnToMenu()
    {
        SceneManager.LoadScene("Start");
    }
}
