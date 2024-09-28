using UnityEngine;
using UnityEngine.InputSystem;

public class Spaceship : MonoBehaviour
{
    [SerializeField] private InputAction _move;
    [SerializeField] private InputAction _shoot;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private float _spaceshipMovementSpeed = 5f;
    [SerializeField] private float _laserCooldown = 0.2f;

    float _timer = 100;

    void Awake()
    {
        _move.Enable();
        _shoot.Enable();
        _shoot.performed += context => TryShoot();
    }

    private void OnDisable()
    {
        _move.Disable();
        _shoot.Disable();
    }
    void FixedUpdate()
    {
        _timer += Time.fixedDeltaTime;

        transform.position += new Vector3(0, _spaceshipMovementSpeed * Time.fixedDeltaTime * _shoot.ReadValue<float>(), 0f);
    }

    void TryShoot()
    {
        if (_timer < _laserCooldown)
        {
            return;
        }

        _timer = 0f;
        Instantiate(_laserPrefab, transform.position, transform.rotation);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Time.timeScale = 0;
    }
}
