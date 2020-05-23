﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float gameOverDelayInSeconds = 3f;

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("SpaceShooterGame");
    }

    public void LoadGameOver()
    {
        StartCoroutine(delayGameOver());
    }

    public void Quit()
    {
        Application.Quit();
    }

    private IEnumerator delayGameOver()
    {
        yield return new WaitForSeconds(gameOverDelayInSeconds);
        SceneManager.LoadScene("GameOverMenu");
    }
}
