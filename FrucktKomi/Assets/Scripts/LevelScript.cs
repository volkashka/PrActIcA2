using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    // ������ �� ������ GameManager.
    [SerializeField] private GameManager _gameManager;

    // ������������� ��������� ���� ��� ������� ������.
    public void SetDifficulty(int difficult)
    {
        // �������� ����� StartGame � ������� GameManager � �������� �������� ���������.
        _gameManager.StartGame(difficult);
    }
}
