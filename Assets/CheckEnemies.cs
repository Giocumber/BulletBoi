using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemies : MonoBehaviour
{
    public bool allEnemiesDestroyed = false;  // Tracks whether all enemies are destroyed
    public GameObject lvlTopImage;
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

        //GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Count == 0 && !allEnemiesDestroyed)
        {
            allEnemiesDestroyed = true;
            OnAllEnemiesDestroyed();
        }
    }

    // This method is called when all enemies are destroyed
    void OnAllEnemiesDestroyed()
    {
        lvlTopImage.SetActive(true);
        lvlCompleteText.SetActive(true);
        textTutor.SetActive(false);

        if(playerObject != null)
        {
            cameraController.isClamped = false;
            cameraZoom.StartZoomIn(6f);
        }

        Invoke("NextLevelBtnPopUp", 1.5f);
    }

    public void LevelFailed()
    {
        lvlTopImage.SetActive(true);
        lvlFailedText.SetActive(true);
        Invoke("RetryBtnPopUp", 1.5f);
    }

    public void NextLevelBtnPopUp()
    {
        nextLvlBtn.SetActive(true);
    }

    public void RetryBtnPopUp()
    {
        retryLvlBtn.SetActive(true);
    }
}
