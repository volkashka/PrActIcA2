using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody _rb;


    private int _minForce = 14;
    private int _maxForces = 10;

    private int minposX = -8;
    private int maxposX = 8;
    private int _randomTorque = 10;

    private GameManager _gameManager;
    [SerializeField] private ParticleSystem _boomEffect;
    [SerializeField] private bool _isDangerous;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _rb = GetComponent<Rigidbody>();
        transform.position = RandomSpawnPosition();

        _rb.AddForce(RandomForce(), ForceMode.Impulse);
        _rb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

    }

    private void FixedUpdate()
    {
        if (transform.position.y < -3)
            Destroy(gameObject);
    }

    private Vector3 RandomForce()
    {

        return Vector3.up * Random.Range(_minForce, _maxForces);
    }

    private Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(minposX, maxposX), 0);

    }

    private float RandomTorque()
    {
        return Random.Range(-_randomTorque, _randomTorque);
    }

    private void OnMouseDown()
    {

        if (_isDangerous)
            _gameManager.DeadMenu();

        _gameManager.AddPoints(5);

        Destroy(gameObject);
        Instantiate(_boomEffect, transform.position, _boomEffect.transform.rotation);

    }


}
