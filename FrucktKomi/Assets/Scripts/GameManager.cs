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
    [SerializeField] private Text _livesText;
    [SerializeField] private GameObject livesText;

    public GameObject pauseButton;


    private void Start()
    {
        pauseButton.gameObject.SetActive(true);
        // Создаем текст с количеством жизней.
        GameManager.Instantiate(_livesText);

        // Устанавливаем начальное количество очков.
        _pointsText.text = "Очки: " + _points;

        // Устанавливаем начальное количество жизней.
        _lives = 3;

        // Получаем максимальный счет из PlayerPrefs.
        _score = PlayerPrefs.GetInt("MaxScore", _score);

        // Устанавливаем текст с максимальным счетом.
        _scoreText.text = "Рекорд: " + _score;
        _livesText.text = "жизни: " + _lives;
 

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

            // Добавляем 1 очко к счету.
            AddPoints(1);
        }
    }

    // Возвращает индекс случайного объекта из списка.
    private int IndexOfRandomObject()
    {
        // Возвращаем случайное число в диапазоне от 0 до количества объектов в списке.
        return Random.Range(0, _targets.Count);
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
        // Сбрасываем количество очков.
        _points = 0;

        // Активируем спавн объектов.
        _spawnActive = true;

        // Запускаем корутину для спавна объектов.
        StartCoroutine(SpawnTarget());

        // Уменьшаем скорость спавна в зависимости от сложности.
        _spawnRate /= difficulty;

        // Скрываем меню уровня.
        _levelMenu.gameObject.SetActive(false);

        livesText.gameObject.SetActive(true);

     

    }


  
    // Сохраняет текущий счет как максимальный счет.
    private void SaveScore()
    {
        // Если текущий счет больше максимального счета, сохраняем его как максимальный счет.
        if (_points > _score)
            PlayerPrefs.SetInt("MaxScore", _points);
    }

    // Отображает меню смерти.
    public void DeadMenu()
    {
        // Отключаем спавн объектов.
        _spawnActive = false;

        // Отображаем текст смерти.
        _deadText.SetActive(true);

      
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
        // Если объект лечебный, прибавляем одну жизнь.
        if (isHeal)
        {
            _lives++;
        }
        // Если объект опасный и у игрока есть жизни, вычитаем одну жизнь.
        else
        {
            if (_lives > 0)
            {
                _lives--;
            }
        }

        // Обновляем текст с количеством жизней.
      
        _livesText.text = _lives.ToString();
        _livesText.text = "жизни: " + _lives;


        // Если у игрока закончились жизни, вызываем меню смерти.
        if (_lives == 0)
        {
            DeadMenu();
        }
    }

  
}