using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Target : MonoBehaviour
{
    // Ссылка на компонент Rigidbody объекта.
    private Rigidbody _rb;

    // Верхняя позиция по оси Y, в которой могут появляться объекты.
    private float _topYPosition = 3f;

    // Нижняя позиция по оси Y, в которой могут появляться объекты.
    private float _bottomYPosition = -13f;

    // Сила, применяемая к объектам, появляющимся снизу.
    private float _upwardForce = 15f;

    // Множитель, уменьшающий силу тяжести для объектов, появляющихся сверху.
    private float _downwardForceMultiplier = 0.1f; // Уменьшаем силу тяжести для объектов, появляющихся сверху.

    // Минимальная сила, которая может быть применена к объекту.
    private int _minForce = 2;

    // Максимальная сила, которая может быть применена к объекту.
    private int _maxForces = 1;

    // Минимальная позиция по оси X, в которой может появиться объект.
    private int minposX = -8;

    // Максимальная позиция по оси X, в которой может появиться объект.
    private int maxposX = 8;

    // Случайный крутящий момент, который может быть применен к объекту.
    private int _randomTorque = 10;

    // Жизни объекта.
    private int _lives = 3;

    // Ссылка на скрипт GameManager.
    private GameManager _gameManager;

    // Эффект взрыва, который будет создан при уничтожении объекта.
    [SerializeField] private ParticleSystem _boomEffect;

    // Флаг, указывающий, является ли объект опасным.
    [SerializeField] private bool _isDangerous;

    // Флаг, указывающий, является ли объект лечебным.
    [SerializeField] private bool _isHeal;

    private void Start()
    {
        // Получаем ссылку на скрипт GameManager.
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // Получаем ссылку на компонент Rigidbody объекта.
        _rb = GetComponent<Rigidbody>();

        // Генерируем случайную позицию по оси X.
        float randomXPosition = Random.Range(minposX, maxposX);

        // Случайным образом выбираем, будет ли объект появляться сверху или снизу.
        bool spawnFromTop = Random.Range(0, 2) == 0;

        // Генерируем случайную позицию по оси Y в зависимости от результата предыдущего выбора.
        float randomYPosition = spawnFromTop ? _topYPosition : _bottomYPosition;

        // Устанавливаем позицию объекта.
        transform.position = new Vector3(randomXPosition, randomYPosition);

        // Если объект появляется снизу, применяем к нему силу вверх.
        if (!spawnFromTop)
        {
            _rb.AddForce(Vector3.up * _upwardForce, ForceMode.Impulse);
        }
        else
        {
            // Если объект появляется сверху, применяем к нему силу вниз с уменьшенной силой тяжести.
            _rb.AddForce(Vector3.down * Random.Range(_minForce, _maxForces) * _downwardForceMultiplier, ForceMode.Impulse);
        }

        // Применяем случайную силу к объекту.
        _rb.AddForce(RandomForce(), ForceMode.Impulse);

        // Применяем случайный крутящий момент к объекту.
        _rb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        // Если объект находится ниже определенной позиции по оси Y, уничтожаем его.
        if (transform.position.y < -18)
            Destroy(gameObject);
    }

    // Возвращает случайную силу.
    private Vector3 RandomForce()
    {
        // Возвращаем вектор силы, направленной вниз, с величиной от минимальной до максимальной.
        return Vector3.down * Random.Range(_minForce, _maxForces);
    }

    // Возвращает случайную позицию появления.
    private Vector3 RandomSpawnPosition()
    {
        // Возвращаем вектор позиции с случайной координатой X в заданном диапазоне и координатой Y равной 0.
        return new Vector3(Random.Range(minposX, maxposX), 0);
    }

    // Возвращает случайный крутящий момент.
    private float RandomTorque()
    {
        // Возвращаем случайное значение крутящего момента в заданном диапазоне.
        return Random.Range(-_randomTorque, _randomTorque);
    }




    private void OnMouseDown()
    {
        // Проверяем, имеет ли объект тег "_isHeal".
        if (_isHeal)
        {
            // Прибавляем одну жизнь.
            _gameManager.UpdateLives(true);
        }
        else if (_isDangerous)
        {
            // Вычитаем одну жизнь.
            _gameManager.UpdateLives(false);
        }
        else
        {
            // Прибавляем 5 очков.
            _gameManager.AddPoints(5);
        }

        // Уничтожаем объект.
        Destroy(gameObject);

        // Создаем эффект взрыва.
        Instantiate(_boomEffect, transform.position, _boomEffect.transform.rotation);
    }


    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, является ли другой объект триггером нижней части экрана.
        if (other.tag == "BottomTrigger")
        {
            if (!_isDangerous)
            {
                // Уменьшаем жизни игрока.
                _gameManager.UpdateLives(false);
            }
        }
    }
}

   





