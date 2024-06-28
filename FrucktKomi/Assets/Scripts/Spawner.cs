using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Список игровых объектов, которые будут появляться.
    [SerializeField] private List<GameObject> _targets;

    // Индекс текущего появляющегося объекта.
    private int _index;

    // Скорость появления объектов.
    [SerializeField] private float _spawnRate = 3.6f;

    // Флаг, указывающий, активен ли спавн объектов.
    private bool _spawnActive;

    public void StartSpawnCoroutine()
    {
        _spawnActive = true;
        StartCoroutine(SpawnTarget());
    }

    public void StopSpawn()
    {
        _spawnActive = false;
    }

    // Корутина, отвечающая за спавн объектов.
    IEnumerator SpawnTarget()
    {
        // Пока спавн активен, выполняем следующие действия:
        while (_spawnActive)
        {
            // Ждем заданное время.
            yield return new WaitForSeconds(_spawnRate);

            // Получаем индекс случайного объекта.
            _index = IndexOfRandomObject();

            // Создаем экземпляр объекта с указанным индексом.
            Instantiate(_targets[_index]);
        }
    }

    // Возвращает индекс случайного объекта из списка.
    private int IndexOfRandomObject()
    {
        // Возвращаем случайное число в диапазоне от 0 до количества объектов в списке.
        return Random.Range(0, _targets.Count);
    }
}