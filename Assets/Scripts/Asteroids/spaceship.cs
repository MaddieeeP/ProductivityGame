using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spaceship : MonoBehaviour
{
    public float spaceship_speed = 5f;
    public float bullet_cooldown = 0.1f;

    public Object bullet_prefab;

    float timer = 100;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(-10,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetKey(KeyCode.W) && (transform.position.y < 4.5)){
            this.transform.position = this.transform.position + new Vector3(0,spaceship_speed * Time.deltaTime,0);
        }
        if (Input.GetKey(KeyCode.S) && (transform.position.y > -4.5)){
            this.transform.position = this.transform.position + new Vector3(0,-spaceship_speed * Time.deltaTime,0);
        }

        if (Input.GetKey(KeyCode.Space)){
            if(timer > bullet_cooldown){
                shoot_gun();
                timer = 0;
            }
        }
    }

    void shoot_gun(){
        Object.Instantiate(bullet_prefab, this.transform.position, new Quaternion(0,0,0,0));
    }
}
