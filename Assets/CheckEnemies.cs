using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckEnemies : MonoBehaviour
{
    public bool allEnemiesDestroyed = false;  // Tracks whether all enemies are destroyed
    public GameObject winTopPanel;
    public GameObject loseTopPanel;
    public GameObject lvlCompleteText;
    public GameObject lvlFailedText;
    public GameObject textTutor;

    // Private reference to another game object
    private GameObject camObject;
    private CameraZoom cameraZoom;
    private CameraController cameraController;

    private GameObject playerObject;
    public GameObject nextLvlBtn;
    public GameObject retryLvlBtn;

    private int buildIndex;
    private Scene currentScene;
    private void Awake()
    {
        // Get the current active scene
        currentScene = SceneManager.GetActiveScene();

        // Get the build index of the current scene
        buildIndex = currentScene.buildIndex;
    }

    private void Start()
    {
        camObject = GameObject.FindGameObjectWithTag("MainCamera");

        if (camObject != null)
        {
            // Get the script (or any other component) from the other game object
            cameraZoom = camObject.GetComponent<CameraZoom>();
            cameraController = camObject.GetComponent<CameraController>();
        }

        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // Check if there are no enemies left in the scene
        List<GameObject> enemies = new List<GameObject>();
        enemies.AddRange(GameObject.FindGameObjectsWithTag("BasicEnemy"));
        enemies.AddRange(GameObject.FindGameObjectsWithTag("SpikedEnemy"));
        enemies.AddRange(GameObject.FindGameObjectsWithTag("GhostEnemy"));
        enemies.AddRange(GameObject.FindGameObjectsWithTag("ShootingEnemy"));

        if (enemies.Count == 0 && !allEnemiesDestroyed)
        {
            allEnemiesDestroyed = true;
            OnAllEnemiesDestroyed();
        }
    }

    // This method is called when all enemies are destroyed
    void OnAllEnemiesDestroyed()
    {
        winTopPanel.SetActive(true);
        lvlCompleteText.SetActive(true);
        textTutor.SetActive(false);

        if (playerObject != null)
        {
            cameraController.isClamped = false;
            cameraZoom.StartZoomIn(6f);
        }

        Invoke("NextLevelBtnPopUp", 1.5f);
        CompleteLevel(buildIndex);
    }

    public void LevelFailed()
    {
        StartCoroutine(LevelFailedCooldown());
    }

    public void NextLevelBtnPopUp()
    {
        nextLvlBtn.SetActive(true);
    }

    public void RetryBtnPopUp()
    {
        retryLvlBtn.SetActive(true);
    }

    public IEnumerator LevelFailedCooldown()
    {
        yield return new WaitForSeconds(1.5f);
        if (!allEnemiesDestroyed)
        {
            loseTopPanel.SetActive(true);
            lvlFailedText.SetActive(true);
            Invoke("RetryBtnPopUp", 1.5f);
        }
    }


    //UNLOCKING LEVELS
    public void CompleteLevel(int currentLevel)
    {
        UpdateUnlockedLevel(currentLevel);
    }

    private void UpdateUnlockedLevel(int currentLevel)
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        if (currentLevel >= unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", currentLevel); // Unlock the next level
            PlayerPrefs.Save(); // Ensure the changes are saved
        }
    }
}

