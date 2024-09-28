using System;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private Transform _canvasTransform;
    [SerializeField] private GameObject _asteroidPrefab;
    [SerializeField] private Bounds _bounds;

    public List<Question> list;

    private float _currentTime = 0f;
    private float _asteroidCooldown = 0.5f;

    void FixedUpdate()
    {
        _currentTime += Time.fixedDeltaTime;

        if (_currentTime % _asteroidCooldown < Time.fixedDeltaTime)
        {
            int questionID = 0; //FIX
            SpawnAsteroid(questionID);
        }
    }

    void SpawnAsteroid(int questionID)
    {
        GameObject asteroidObject = Instantiate(_asteroidPrefab, new Vector3(_bounds.center.x, (float)Math.Sin(_currentTime) * 0.5f * _bounds.size.y, 0f), Quaternion.identity, _canvasTransform);
        asteroidObject.GetComponent<Asteroid>().questionID = questionID;
    }
}