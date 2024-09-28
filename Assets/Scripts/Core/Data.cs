using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public List<Question> questions = new List<Question>();

    // Start is called before the first frame update
    void Awake()
    {
        LoadData();

        /*AddQuestion("What is 1+1?", "2");
        AddQuestion("What is 1+2?", "3");
        AddQuestion("What is 1+3?", "4");
        AddQuestion("What is 1+4?", "5");
        AddQuestion("What is 1+5?", "6");
        AddQuestion("What is 1+6?", "7");
        AddQuestion("What is 1+7?", "8");
        AddQuestion("What is 1+8?", "9");

        SaveData();

        foreach (Question q in questions)
        {
            print(q.qText);
            print(q.aText);
        }*/
    }

    private void LoadData()
    {
        questions = new List<Question>();
        int count = PlayerPrefs.GetInt("count");
        int j = 0;
        for (int i = 0; i < count; i++)
        {
            string q = PlayerPrefs.GetString(j.ToString());
            j++;
            string a = PlayerPrefs.GetString(j.ToString());
            j++;

            AddQuestion(q, a);
        }
    }

    public void SaveData() //Janky AF but should work
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("count", questions.Count);
        int j = 0;
        for (int i = 0; i < questions.Count; i++)
        {
            PlayerPrefs.SetString(j.ToString(), questions[i].qText);
            j++;
            PlayerPrefs.SetString(j.ToString(), questions[i].aText);
            j++;
        }
    }

    public void AddQuestion(string q, string a)
    {
        questions.Add(new Question(q, a));
    }

    public void EditQuestion(string qOld, string qNew = "", string aNew = "")
    {
        int i = 0;
        foreach (Question question in questions) 
        {
            if (question.qText.ToLower() == qOld.ToLower())
            {
                break;
            }
            i++;
        }

        questions.RemoveAt(i);

        if (qNew != "")
        {
            AddQuestion(qNew, aNew);
        }
    }

    public List<Question> GetQuestions() 
    {
        return questions;
    }

    public Question RandomQuestion()
    {
        LoadData();
        return questions[Random.Range(0, questions.Count)];
    }
}
