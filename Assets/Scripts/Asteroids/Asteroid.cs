using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private int _questionID;

    public int questionID { set { _questionID = value; } }

    void Start()
    {
        transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-AsteroidGameManager.Instance.asteroidMinSpeed, -AsteroidGameManager.Instance.asteroidMaxSpeed), 0f), ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        AsteroidGameManager.Instance.OnAsteroidHit(_questionID);
        Destroy(collider.gameObject);
        Destroy(gameObject);
    }
}
