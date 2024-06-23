using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    // ������ ������� ��������, ������� ����� ����������.
    [SerializeField] private List<GameObject> _targets;

    // �����, ������������ ���������� �����.
    [SerializeField] private Text _pointsText;

    // �����, ������������ ����.
    [SerializeField] private Text _scoreText;

    // ������ �������� ������������� �������.
    private int _index;

    // ���������� �����.
    private int _points;

    // ����.
    [SerializeField] private int _score;

    // �������� ��������� ��������.
    [SerializeField] private float _spawnRate = 3.6f;

    // �����, ������������ ��� ������ ������.
    [SerializeField] private GameObject _deadText;

    // ���� ������.
    [SerializeField] private GameObject _levelMenu;

    // ����, �����������, ������� �� ����� ��������.
    private bool _spawnActive;

    // ����� ������.
    public int _lives;

    // �����, ������������ ���������� ������.
    [SerializeField] private Text _livesText;
    [SerializeField] private GameObject livesText;

    public GameObject pauseButton;


    private void Start()
    {
        pauseButton.gameObject.SetActive(true);
        // ������� ����� � ����������� ������.
        GameManager.Instantiate(_livesText);

        // ������������� ��������� ���������� �����.
        _pointsText.text = "����: " + _points;

        // ������������� ��������� ���������� ������.
        _lives = 3;

        // �������� ������������ ���� �� PlayerPrefs.
        _score = PlayerPrefs.GetInt("MaxScore", _score);

        // ������������� ����� � ������������ ������.
        _scoreText.text = "������: " + _score;
        _livesText.text = "�����: " + _lives;
 

    }


    // ��������, ���������� �� ����� ��������.
    IEnumerator SpawnTarget()
    {
        // ���� ����� �������, ��������� ��������� ��������:
        while (_spawnActive)
        {
            // ���� �������� �����.
            yield return new WaitForSeconds(_spawnRate);

            // �������� ������ ���������� �������.
            _index = IndexOfRandomObject();

            // ������� ��������� ������� � ��������� ��������.
            Instantiate(_targets[_index]);

            // ��������� 1 ���� � �����.
            AddPoints(1);
        }
    }

    // ���������� ������ ���������� ������� �� ������.
    private int IndexOfRandomObject()
    {
        // ���������� ��������� ����� � ��������� �� 0 �� ���������� �������� � ������.
        return Random.Range(0, _targets.Count);
    }


    // ��������� ��������� ���������� ����� � �������� ����� � ��������� ����� � ������.
    public void AddPoints(int pointsToAdd)
    {
        _points += pointsToAdd;
        _pointsText.text = "����: " + _points;
    }

    // �������� ���� � ��������� ����������.
    public void StartGame(int difficulty)
    {
        // ���������� ���������� �����.
        _points = 0;

        // ���������� ����� ��������.
        _spawnActive = true;

        // ��������� �������� ��� ������ ��������.
        StartCoroutine(SpawnTarget());

        // ��������� �������� ������ � ����������� �� ���������.
        _spawnRate /= difficulty;

        // �������� ���� ������.
        _levelMenu.gameObject.SetActive(false);

        livesText.gameObject.SetActive(true);

     

    }


  
    // ��������� ������� ���� ��� ������������ ����.
    private void SaveScore()
    {
        // ���� ������� ���� ������ ������������� �����, ��������� ��� ��� ������������ ����.
        if (_points > _score)
            PlayerPrefs.SetInt("MaxScore", _points);
    }

    // ���������� ���� ������.
    public void DeadMenu()
    {
        // ��������� ����� ��������.
        _spawnActive = false;

        // ���������� ����� ������.
        _deadText.SetActive(true);

      
    }

    // ������������� ����.
    public void RestartGame()
    {
        // ��������� ������� ����� ������.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ��� ������ �� ���������� ��������� ������� ���� ��� ������������ ����.
    private void OnApplicationQuit()
    {
        SaveScore();
    }

    // ��������� ���������� ������ � ����������� �� ����, �������� �� ������ �������� ��� �������.
    public void UpdateLives(bool isHeal)
    {
        // ���� ������ ��������, ���������� ���� �����.
        if (isHeal)
        {
            _lives++;
        }
        // ���� ������ ������� � � ������ ���� �����, �������� ���� �����.
        else
        {
            if (_lives > 0)
            {
                _lives--;
            }
        }

        // ��������� ����� � ����������� ������.
      
        _livesText.text = _lives.ToString();
        _livesText.text = "�����: " + _lives;


        // ���� � ������ ����������� �����, �������� ���� ������.
        if (_lives == 0)
        {
            DeadMenu();
        }
    }

  
}