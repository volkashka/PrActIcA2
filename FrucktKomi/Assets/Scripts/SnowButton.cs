using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowButton : MonoBehaviour
{

    public GameObject pauseButton;

    public void ShowButton()
    {
        pauseButton.gameObject.SetActive(true);
    }
}