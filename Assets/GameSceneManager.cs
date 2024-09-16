using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    int sceneNum;

    public void NextScene()
    {
        sceneNum++;
        SceneManager.LoadScene(sceneNum);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
       Application.Quit();
    }

}
