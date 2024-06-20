using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    // Ссылка на скрипт GameManager.
    [SerializeField] private GameManager _gameManager;

    // Устанавливает сложность игры при нажатии кнопки.
    public void SetDifficulty(int difficult)
    {
        // Вызывает метод StartGame в скрипте GameManager и передает значение сложности.
        _gameManager.StartGame(difficult);
    }
}
