using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float bullet_speed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(bullet_speed * Time.deltaTime, 0, 0);
        if (this.transform.position.x > 14){
            Destroy(gameObject);
        }
    }
}
