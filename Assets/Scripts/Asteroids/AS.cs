using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AS : MonoBehaviour
{

    public Object asteroid_prefab;

    public List<Question> list;

    public float ast_cd = 0.5f;
    float question_cd = 0f;

    float timer_ast = 100;
    float timer_q = 100;

    public TMP_Text question_txt;
    public TMP_Text score_txt;
    public TMP_Text a;
    public TMP_Text b;
    public static int score = 0;
    public static char current_ans = 'a';


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer_ast += Time.deltaTime;
        timer_q += Time.deltaTime;


        if(timer_ast > ast_cd){
            spawn_ast();
            timer_ast = 0;
        }

        if(timer_q > question_cd){
            get_question();
            timer_q = 0;
            question_cd = Random.Range(3,8);
        }

        score_txt.SetText("Score: " + score);
    }

    void spawn_ast(){
        Object.Instantiate(asteroid_prefab, new Vector3( 14, Random.Range(-4,4),0), new Quaternion(0,0,0,0));
    }

    void get_question(){
        int n1 = Random.Range(1,100);
        int n2 = Random.Range(1,100);

        question_txt.SetText("What is " + n1 + " + " + n2 +"?");

        int ans = n1+n2;
        int faker = ans + Random.Range(-5,5);

        if (Random.value > 0.5){
            current_ans = 'a';
            a.SetText(""+ans);
            b.SetText(""+faker);
        } else{
            current_ans = 'b';
            a.SetText(""+faker);
            b.SetText(""+ans);
        }    
    }
}
