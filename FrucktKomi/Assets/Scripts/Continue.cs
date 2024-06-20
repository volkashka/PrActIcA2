using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continue : MonoBehaviour
{
    public GameObject panel;

    public void Next()
    {
        Time.timeScale = 1f;
        panel.SetActive(false);
    }

}
