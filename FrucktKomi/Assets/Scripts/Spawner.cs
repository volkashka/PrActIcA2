using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // ������ ������� ��������, ������� ����� ����������.
    [SerializeField] private List<GameObject> _targets;

    // ������ �������� ������������� �������.
    private int _index;

    // �������� ��������� ��������.
    [SerializeField] private float _spawnRate = 3.6f;

    // ����, �����������, ������� �� ����� ��������.
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

    // ��������, ���������� �� ����� ��������.
    IEnumerator SpawnTarget()
    {
        // ���� ����� �������, ��������� ��������� ��������:
        while (_spawnActive)
        {
            // ���� �������� �����.
            yield return new WaitForSeconds(_spawnRate);

            // �������� ������ ���������� �������.
            _index = IndexOfRandomObject();

            // ������� ��������� ������� � ��������� ��������.
            Instantiate(_targets[_index]);
        }
    }

    // ���������� ������ ���������� ������� �� ������.
    private int IndexOfRandomObject()
    {
        // ���������� ��������� ����� � ��������� �� 0 �� ���������� �������� � ������.
        return Random.Range(0, _targets.Count);
    }
}