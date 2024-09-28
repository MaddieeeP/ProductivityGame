using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.Examples;
using UnityEngine.SceneManagement;

public class AngryCrowMG : MonoBehaviour
{
    public Transform hut;
    public Transform canvas;

    List<Transform> huts;
    int answerHut;
    bool answered = false;
    bool correct = false;
    bool firstHutCorrect = true;

    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        Data data = transform.GetComponent<Data>();

        score = PlayerPrefs.GetInt("Score");

        //Spawn huts
        huts = new List<Transform>();
        huts.Add(Instantiate(hut, transform));
        huts.Add(Instantiate(hut, transform));

        float y = -3.5f;
        huts[0].position = new Vector2(-6, y);
        huts[1].position = new Vector2(6, y);

        SetColour(huts[0], new Color(0.15f, 0.15f, 0.15f));
        SetColour(huts[1], new Color(0.15f, 0.15f, 0.15f));

        //Get Q
        Question q = data.RandomQuestion();

        canvas.GetChild(0).GetComponent<TMP_Text>().text = q.qText;

        int firstIndex = Random.Range(0, 2);
        print(firstIndex);

        firstHutCorrect = firstIndex == 0;

        int secondIndex = 1 - firstIndex;

        canvas.GetChild(firstIndex + 1).GetComponent<TMP_Text>().text = q.aText;
        answerHut = firstIndex;

        //Getting incorrect answer
        q = data.RandomQuestion();
        canvas.GetChild(secondIndex + 1).GetComponent<TMP_Text>().text = q.aText;

        StartCoroutine("Timeout");
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < huts.Count; i++) 
        {
            if (maxVel(huts[i]) >= 1f && !answered) 
            {
                print("hut " + i.ToString() + " has fallen!");

                if (i == answerHut) 
                {
                    print("Correct answer!");
                    correct = true;
                    score++;
                    StartCoroutine("ResultAndRestart");
                }
                else
                {
                    print("Incorrect answer!");
                    StartCoroutine("ResultAndRestart");
                }

                answered = true;
            }
        }

        canvas.GetChild(3).GetComponent<TMP_Text>().text = "Score: " + score.ToString();
    }

    private void SetColour(Transform h, Color c)
    {
        foreach (SpriteRenderer sr in h.GetComponentsInChildren<SpriteRenderer>())
        {
            sr.color = c;
        }
    }

    private float maxVel(Transform h)
    {
        float maxV = 0;
        foreach (Rigidbody2D r in h.GetComponentsInChildren<Rigidbody2D>())
        {
            if (r.velocity.magnitude > maxV)
            {
                maxV = r.velocity.magnitude;
            }
        }

        return maxV;
    }

    IEnumerator ResultAndRestart()
    {

        for (int i = 0; i < 100; i++) 
        {
            Color colCorrect = new Color(0, 1, 0);
            Color colIncorrect = new Color(250/255f, 50/255f, 0);

            Color col;
            Color col2;

            if (firstHutCorrect)
            {
                col = colCorrect;
                col2 = colIncorrect;
            }
            else 
            {
                col2 = colCorrect;
                col = colIncorrect;
            }

            SetColour(huts[0], lerpCol(new Color(0.15f, 0.15f, 0.15f), col, i/100f));
            SetColour(huts[1], lerpCol(new Color(0.15f, 0.15f, 0.15f), col2, i/100f));

            yield return new WaitForSeconds(0.002f);
        }

        yield return new WaitForSeconds(1f);
        PlayerPrefs.SetInt("Score", score);
        Reload();
    }

    private Color lerpCol(Color start, Color end, float t)
    {
        return new Color(
            Mathf.Lerp(start.r, end.r, t), 
            Mathf.Lerp(start.g, end.g, t), 
            Mathf.Lerp(start.b, end.b, t), 
            Mathf.Lerp(start.a, end.a, t));
    }

    public static void Reload() 
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    IEnumerator Timeout()
    {
        yield return new WaitForSeconds(12.5f);

        Reload();
    }
}
