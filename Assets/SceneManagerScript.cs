using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public bool isPaused;

    public GameObject pauseMenuPanel;

    private void Start()
    {
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void MainMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartCurrentScene()
    {
        // Get the active scene and reload it
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void LoadNextScene()
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the next scene by incrementing the current scene's index
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenuPanel.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
        pauseMenuPanel.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    } 
}
