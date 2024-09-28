using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class astroid_sc : MonoBehaviour
{
    public float max_rot_spd;
    public float max_ast_spd;

    public SpriteRenderer sadge;

    float rot_spd = 1f;
    float ast_spd = 1f;

    char i_am = 'a';

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rot_spd = Random.Range(-max_rot_spd, max_rot_spd);
        ast_spd = Random.Range(1, max_ast_spd);

        if (Random.value > 0.5) {
            i_am = 'b';
            sadge.color = Color.green;
        }

        rb.MovePosition(new Vector3(Random.Range(-ast_spd, -1) * Time.deltaTime,0,0));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(Random.Range(-rot_spd, rot_spd) * Time.deltaTime,0,0));
    }

    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.tag == "burret") {
            if (AS.current_ans == i_am){
                AS.score += 1;
            }
            else{
                AS.score -= 1;
            }
            Object.Destroy(col.gameObject);
        }
    }
}
