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

        // Set screen size for Standalone
        #if UNITY_STANDALONE
                Screen.SetResolution(540, 960, false);
                Screen.fullScreen = false;
        #endif
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
