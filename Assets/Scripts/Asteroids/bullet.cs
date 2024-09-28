using UnityEngine;

public class Bullet : MonoBehaviour
{
    public void FixedUpdate()
    {
        transform.position += new Vector3(AsteroidGameManager.Instance.laserSpeed * Time.fixedDeltaTime, 0, 0);
        
        if (transform.position.x > 2500)
        {
            Destroy(gameObject);
        }
    }
}
