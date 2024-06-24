using TMPro;
using UnityEngine;

public class Timerr : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float ellapsedTime;
    private GameManager _gameManager;

    void Start()
    {
        // Получение ссылки на объект GameManager
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (ellapsedTime > 0)
        {
            ellapsedTime -= Time.deltaTime;
        }
        else if (ellapsedTime < 0)
        {
            ellapsedTime = 0;
        }
        int minutes= Mathf.FloorToInt(ellapsedTime/60);
        int second=Mathf.FloorToInt(ellapsedTime%60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, second);

        // Проверка, закончился ли таймер
        if (ellapsedTime == 0)
        {
            // Вызов метода DeadMenu() из класса GameManager
            _gameManager.DeadMenu();
        }
    }
}
