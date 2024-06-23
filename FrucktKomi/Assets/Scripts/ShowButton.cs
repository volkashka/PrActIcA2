using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowButton : MonoBehaviour
{
    public GameObject pauseButton; // ��������� ���������� ��� ������ �� ������ �����

    private void Start()
    {
        // ������� ������ ����� ��� ������� ����
        pauseButton.SetActive(false);
    }

    public void OnClick()
    {
        // ����� ������ ����� ��� ������� �� ������ "������"
        pauseButton.SetActive(true);
    }
}
