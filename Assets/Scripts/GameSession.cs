using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int gameScore = 0;

    private void Awake()
    {
        SetupSingleton();
    }

    private void SetupSingleton()
    {
        if(FindObjectsOfType<GameSession>().Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            Debug.Log("session destroyed");
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Debug.Log("session maintained");
        }
    }

    public int GetScore()
    {
        return gameScore;
    }

    public void AddToScore(int scoreToAdd)
    {
        gameScore += scoreToAdd;
    }

    public void Reset()
    {
        Destroy(gameObject);
    }
}
