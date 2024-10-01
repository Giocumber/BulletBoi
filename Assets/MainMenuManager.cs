using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuManager : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void LoadNextScene()
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the next scene by incrementing the current scene's index
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void Exit()
    {
        Application.Quit();
        ResetUnlockedLevel();
    }

    void ResetUnlockedLevel()
    {
        // Reset the PlayerPrefs entry for "UnlockedLevel" to 1
        PlayerPrefs.SetInt("UnlockedLevel", 1);
        PlayerPrefs.Save(); // Save the changes to PlayerPrefs

        Debug.Log("UnlockedLevel has been reset to 1.");
    }

}
