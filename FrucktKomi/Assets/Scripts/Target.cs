using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Target : MonoBehaviour
{
    // ������ �� ��������� Rigidbody �������.
    private Rigidbody _rb;

    // ������� ������� �� ��� Y, � ������� ����� ���������� �������.
    private float _topYPosition = 3f;

    // ������ ������� �� ��� Y, � ������� ����� ���������� �������.
    private float _bottomYPosition = -13f;

    // ����, ����������� � ��������, ������������ �����.
    private float _upwardForce = 15f;

    // ���������, ����������� ���� ������� ��� ��������, ������������ ������.
    private float _downwardForceMultiplier = 0.1f; // ��������� ���� ������� ��� ��������, ������������ ������.

    // ����������� ����, ������� ����� ���� ��������� � �������.
    private int _minForce = 2;

    // ������������ ����, ������� ����� ���� ��������� � �������.
    private int _maxForces = 1;

    // ����������� ������� �� ��� X, � ������� ����� ��������� ������.
    private int minposX = -8;

    // ������������ ������� �� ��� X, � ������� ����� ��������� ������.
    private int maxposX = 8;

    // ��������� �������� ������, ������� ����� ���� �������� � �������.
    private int _randomTorque = 10;

    // ����� �������.
    private int _lives = 3;

    // ������ �� ������ GameManager.
    private GameManager _gameManager;

    // ������ ������, ������� ����� ������ ��� ����������� �������.
    [SerializeField] private ParticleSystem _boomEffect;

    // ����, �����������, �������� �� ������ �������.
    [SerializeField] private bool _isDangerous;

    // ����, �����������, �������� �� ������ ��������.
    [SerializeField] private bool _isHeal;

    private void Start()
    {
        // �������� ������ �� ������ GameManager.
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // �������� ������ �� ��������� Rigidbody �������.
        _rb = GetComponent<Rigidbody>();

        // ���������� ��������� ������� �� ��� X.
        float randomXPosition = Random.Range(minposX, maxposX);

        // ��������� ������� ��������, ����� �� ������ ���������� ������ ��� �����.
        bool spawnFromTop = Random.Range(0, 2) == 0;

        // ���������� ��������� ������� �� ��� Y � ����������� �� ���������� ����������� ������.
        float randomYPosition = spawnFromTop ? _topYPosition : _bottomYPosition;

        // ������������� ������� �������.
        transform.position = new Vector3(randomXPosition, randomYPosition);

        // ���� ������ ���������� �����, ��������� � ���� ���� �����.
        if (!spawnFromTop)
        {
            _rb.AddForce(Vector3.up * _upwardForce, ForceMode.Impulse);
        }
        else
        {
            // ���� ������ ���������� ������, ��������� � ���� ���� ���� � ����������� ����� �������.
            _rb.AddForce(Vector3.down * Random.Range(_minForce, _maxForces) * _downwardForceMultiplier, ForceMode.Impulse);
        }

        // ��������� ��������� ���� � �������.
        _rb.AddForce(RandomForce(), ForceMode.Impulse);

        // ��������� ��������� �������� ������ � �������.
        _rb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        // ���� ������ ��������� ���� ������������ ������� �� ��� Y, ���������� ���.
        if (transform.position.y < -18)
            Destroy(gameObject);
    }

    // ���������� ��������� ����.
    private Vector3 RandomForce()
    {
        // ���������� ������ ����, ������������ ����, � ��������� �� ����������� �� ������������.
        return Vector3.down * Random.Range(_minForce, _maxForces);
    }

    // ���������� ��������� ������� ���������.
    private Vector3 RandomSpawnPosition()
    {
        // ���������� ������ ������� � ��������� ����������� X � �������� ��������� � ����������� Y ������ 0.
        return new Vector3(Random.Range(minposX, maxposX), 0);
    }

    // ���������� ��������� �������� ������.
    private float RandomTorque()
    {
        // ���������� ��������� �������� ��������� ������� � �������� ���������.
        return Random.Range(-_randomTorque, _randomTorque);
    }




    private void OnMouseDown()
    {
        // ���������, ����� �� ������ ��� "_isHeal".
        if (_isHeal)
        {
            // ���������� ���� �����.
            _gameManager.UpdateLives(true);
        }
        else if (_isDangerous)
        {
            // �������� ���� �����.
            _gameManager.UpdateLives(false);
        }
        else
        {
            // ���������� 5 �����.
            _gameManager.AddPoints(5);
        }

        // ���������� ������.
        Destroy(gameObject);

        // ������� ������ ������.
        Instantiate(_boomEffect, transform.position, _boomEffect.transform.rotation);
    }


    private void OnTriggerEnter(Collider other)
    {
        // ���������, �������� �� ������ ������ ��������� ������ ����� ������.
        if (other.tag == "BottomTrigger")
        {
            if (!_isDangerous)
            {
                // ��������� ����� ������.
                _gameManager.UpdateLives(false);
            }
        }
    }
}

   





