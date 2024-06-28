using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class LevelScript : MonoBehaviour
{
    // ������ �� ������ GameManager.
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Timerr timerr;
    public GameObject Timer;
    public static LevelScript Instance;


    // ������������� ��������� ���� ��� ������� ������.
    public void SetDifficulty(Difficulty difficulty)
    {
        // ����������� ��������� ��������� �� ���������� ButtonDifficulty.
        switch (difficulty)
        {
            case Difficulty.Easy:
                _gameManager.StartGame(Difficulty.Easy);
                break;
            case Difficulty.Medium:
                _gameManager.StartGame(Difficulty.Medium);
                break;
            case Difficulty.Hard:
                _gameManager.StartGame(Difficulty.Hard);
                break;
            default:
                _gameManager.StartGame(Difficulty.Medium);
                break;
        }
   

        _gameManager._livesText.gameObject.SetActive(true);
        _gameManager._livesText.text = "�����: " + _gameManager._lives;

    }

    public void SetEasyDifficulty()
    {
        SetDifficulty(Difficulty.Easy);
    }

    public void SetMediumDifficulty()
    {
        SetDifficulty(Difficulty.Medium);
    }

    public void SetHardDifficulty()
    {
        SetDifficulty(Difficulty.Hard);
    }

}
