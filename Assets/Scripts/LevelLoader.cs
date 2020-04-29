using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadMainGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
        SceneManager.LoadScene("Game Over");
    }

    IEnumerator WaitAndLoad()
    {

    }
    public void QiutGame()
    {
        Application.Quit();
    }

}
