using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    // Список игровых объектов, которые будут появляться.
    [SerializeField] private List<GameObject> _targets;

    // Текст, отображающий количество очков.
    [SerializeField] private Text _pointsText;

    // Текст, отображающий счет.
    [SerializeField] private Text _scoreText;

    [SerializeField] private Spawner spawner;
    // Индекс текущего появляющегося объекта.
    private int _index;

    // Количество очков.
    private int _points;

    // Счет.
    [SerializeField] private int _score;

    // Скорость появления объектов.
    [SerializeField] private float _spawnRate = 3.6f;

    // Текст, отображаемый при смерти игрока.
    [SerializeField] private GameObject _deadText;

    // Меню уровня.
    [SerializeField] private GameObject _levelMenu;

    // Флаг, указывающий, активен ли спавн объектов.
    private bool _spawnActive;

    // Жизни игрока.
    public int _lives;

    // Текст, отображающий количество жизней.
    [SerializeField] public Text _livesText;
    [SerializeField] public GameObject livesText;

    public GameObject pauseButton;

 
    private void Start()
    {
        pauseButton.gameObject.SetActive(true);

        // Устанавливаем начальное количество очков.
        _pointsText.text = "Очки: " + _points;

        // Устанавливаем начальное количество жизней.
        _lives = 3;

        // Получаем максимальный счет из PlayerPrefs.
        _score = PlayerPrefs.GetInt("MaxScore", _score);

        // Устанавливаем текст с максимальным счетом.
        _scoreText.text = "Рекорд: " + _score;
        _livesText.text = "Жизни: " + _lives;
       


    }

    // Добавляет указанное количество очков к текущему счету и обновляет текст с очками.
    public void AddPoints(int pointsToAdd)
    {
        _points += pointsToAdd;
        _pointsText.text = "Очки: " + _points;
    }

    // Начинает игру с указанной сложностью.
    public void StartGame(int difficulty)
    {
        _points = 0;

        _spawnActive = true;

        // Запускаем корутину для спавна объектов.
        spawner.StartSpawnCoroutine();

        // Уменьшаем скорость спавна в зависимости от сложности.
        _spawnRate /= difficulty;

        // Скрываем меню уровня.
        _levelMenu.gameObject.SetActive(false);

        livesText.gameObject.SetActive(true);

    }

    // Сохраняет текущий счет как максимальный счет.
    private void SaveScore()
    {
        if (_points > _score)
            PlayerPrefs.SetInt("MaxScore", _points);
    }

    public void DeadMenu()
    {
        _spawnActive = false;

        _deadText.SetActive(true);
        spawner.StopSpawn();

    }

    // Перезапускает игру.
    public void RestartGame()
    {
        // Загружаем текущую сцену заново.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // При выходе из приложения сохраняет текущий счет как максимальный счет.
    private void OnApplicationQuit()
    {
        SaveScore();
    }

    // Обновляет количество жизней в зависимости от того, является ли объект лечебным или опасным.
    public void UpdateLives(bool isHeal)
    {
        if (isHeal)
        {
            _lives++;
        }
        else
        {
            if (_lives > 0)
            {
                _lives--;
            }
        }

        // Обновляем текст с количеством жизней.
      
        _livesText.text = _lives.ToString();
        _livesText.text = "Жизни: " + _lives;

        // Если у игрока закончились жизни, вызываем меню смерти.
        if (_lives == 0)
        {
            DeadMenu();
        }
    }
}