using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    // Ссылка на скрипт GameManager.
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Timerr timerr;
    public GameObject Timer;

    // Устанавливает сложность игры при нажатии кнопки.
    public void SetDifficulty(int difficult)
    {
        timerr.isTimerRunning = true; // Запускаем таймер

        // Вызывает метод StartGame в скрипте GameManager и передает значение сложности.
        _gameManager.StartGame(difficult);

        _gameManager._livesText.gameObject.SetActive(true);


    }
}
