using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{


    public GameObject textObject;
    public GameObject panel;



    private void Start()

    {
        textObject = GameObject.Find("TextObject");

        // Скрытие текстового объекта по умолчанию
        textObject.SetActive(false);
    }

    public void OnClick()
    {
        // Показ текстового объекта
        textObject.SetActive(true);
    }

    public void pause()
    {
        panel.SetActive(true);
        Time.timeScale = 0;
    }

}