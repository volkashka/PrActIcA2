using TMPro;
using UnityEngine;

public class Timerr : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float ellapsedTime;
    private GameManager _gameManager;
    public bool isTimerRunning;


    void Start()
    {
        // Получение ссылки на объект GameManager
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        // Проверяем, запущен ли таймер
        if (isTimerRunning)
        {
            if (ellapsedTime > 0)
            {
                ellapsedTime -= Time.deltaTime;
            }
            else if (ellapsedTime < 0)
            {
                ellapsedTime = 0;
            }
            int minutes = Mathf.FloorToInt(ellapsedTime / 60);
            int seconds = Mathf.FloorToInt(ellapsedTime % 60);

            timerText.text = string.Format("Осталось времени   {0:00}:{1:00}", minutes, seconds);

            // Проверка, закончился ли таймер
            if (ellapsedTime == 0)
            {
                // Вызов метода DeadMenu() из класса GameManager
                _gameManager.DeadMenu();
                isTimerRunning = false; // Останавливаем таймер после завершения времени
            }
        }
    }
}
