using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow : MonoBehaviour
{
    private Rigidbody2D r;
    bool launched = false;
    bool invis = false;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody2D>();
        r.simulated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!launched)
        {
            Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

            Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
            float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + 180));

            if (Input.GetMouseButtonDown(0)) 
            {
                r.simulated = true;
                launched = true;
                r.AddForce(transform.right * 500);
            }
        }
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    void OnBecameInvisible()
    {
        invis = true;
        print("Bird out of view");
        StartCoroutine("OutOfViewDelete");
    }

    void OnBecameVisible()
    {
        print("Bird in view");
        invis = false;
    }
    IEnumerator OutOfViewDelete()
    {
        yield return new WaitForSeconds(3.0f);

        if (invis) { AngryCrowMG.Reload(); }
    }
}
