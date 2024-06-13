using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _targets;

    [SerializeField] private Text _pointsText;
    [SerializeField] private Text _scoreText;

    private int _index;
    private int _points;
    [SerializeField] private int _score;

    private float _spawnRate = 3.6f;

    [SerializeField] private GameObject _deadText;
    [SerializeField] private GameObject _levelMenu;

    private bool _spawnActive;

    private void Start()
    {
        _pointsText.text = "����: " + _points;

        _score = PlayerPrefs.GetInt("MaxScore", _score);
        _scoreText.text = "������: " + _score;
    }

    IEnumerator SpawnTarget()
    {
        while (_spawnActive)
        {
            yield return new WaitForSeconds(_spawnRate);
            _index = IndexOfRandomObject();
            
            Instantiate(_targets[_index]);
            AddPoints(1);
        }

    
    }

    private int IndexOfRandomObject()
    {
        return Random.Range(0, _targets.Count);

    }

    public void AddPoints(int pointsToAdd)
    {
        _points += pointsToAdd;
        _pointsText.text = "����: " + _points;
    }

    public void StartGame(int difficulty)
    {
        _points = 0;
        _spawnActive = true;

        StartCoroutine(SpawnTarget());

   
        _spawnRate /= difficulty;
        _levelMenu.gameObject.SetActive(false);

    }

    private void SaveScore()
    {

        if (_points > _score)
            PlayerPrefs.SetInt("MaxScore", _points);

    }
    
    public void DeadMenu()
    {

        _spawnActive = false;
        _deadText.SetActive(true);

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    private void OnApplicationQuit()
    {
        SaveScore();
    }


}
