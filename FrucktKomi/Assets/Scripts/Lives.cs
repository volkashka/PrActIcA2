using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    private GameManager _gameManager;
    private int _lives;
    void Start()
    {
        
    }
    public void AddLives()
    {
        _lives =+ 1;
    }
    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, является ли другой объект игроком.
        if (other.gameObject.CompareTag("Player"))
        {
            // Прибавляем одну жизнь.
            AddLives();
        }
    }





}
