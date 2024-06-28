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
        // ��������� ������ �� ������ GameManager
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        // ���������, ������� �� ������
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

            timerText.text = string.Format("�������� �������   {0:00}:{1:00}", minutes, seconds);

            // ��������, ���������� �� ������
            if (ellapsedTime == 0)
            {
                // ����� ������ DeadMenu() �� ������ GameManager
                _gameManager.DeadMenu();
                isTimerRunning = false; // ������������� ������ ����� ���������� �������
            }
        }
    }
}
