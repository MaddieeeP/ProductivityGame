using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AsteroidGameManager : MonoBehaviour
{
    public static AsteroidGameManager Instance { get; private set; }

    [SerializeField] private float _asteroidMinSpeed;
    [SerializeField] private float _asteroidMaxSpeed;
    [SerializeField] private float _laserSpeed;

    [SerializeField] private TMP_Text _questionText;
    [SerializeField] private TMP_Text _scoreText;

    int _score = 0;
    int _currentQuestion;

    public float asteroidMinSpeed { get { return _asteroidMinSpeed; } }
    public float asteroidMaxSpeed { get { return _asteroidMaxSpeed; } }
    public float laserSpeed { get { return _laserSpeed; } }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }

        Destroy(this.gameObject);
    }
    public void Update()
    {
        _scoreText.SetText("Score: " + _score);
    }

    public void OnAsteroidHit(int asteroidID)
    {
        if (asteroidID == _currentQuestion)
        {
            _score++;
            return;
        }
        _score = Math.Max(0, _score - 1);
    }
}
