using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowButton : MonoBehaviour
{
    public GameObject pauseButton; // Публичная переменная для ссылки на кнопку паузы

    private void Start()
    {
        // Скрытие кнопки паузы при запуске игры
        pauseButton.SetActive(false);
    }

    public void OnClick()
    {
        // Показ кнопки паузы при нажатии на кнопку "Играть"
        pauseButton.SetActive(true);
    }
}
