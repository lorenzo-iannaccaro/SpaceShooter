using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
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
        SceneManager.LoadScene("Game Over");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
