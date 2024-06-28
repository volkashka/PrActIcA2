using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    // ������ �� ������ GameManager.
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Timerr timerr;
    public GameObject Timer;

    // ������������� ��������� ���� ��� ������� ������.
    public void SetDifficulty(int difficult)
    {
        timerr.isTimerRunning = true; // ��������� ������

        // �������� ����� StartGame � ������� GameManager � �������� �������� ���������.
        _gameManager.StartGame(difficult);

        _gameManager._livesText.gameObject.SetActive(true);


    }
}
