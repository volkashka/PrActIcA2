using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject panel;

    public void pause()
    {
        panel.SetActive(true);
        Time.timeScale = 0;
    }

}