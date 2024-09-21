using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemies : MonoBehaviour
{
    public bool allEnemiesDestroyed = false;  // Tracks whether all enemies are destroyed
    public GameObject lvlCompleteImage;
    public GameObject lvlCompleteText;
    public GameObject textTutor;

    // Private reference to another game object
    private GameObject camObject;
    private CameraZoom cameraZoom;
    private CameraController cameraController;

    private GameObject playerObject;

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
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0 && !allEnemiesDestroyed)
        {
            allEnemiesDestroyed = true;
            OnAllEnemiesDestroyed();
        }
    }

    // This method is called when all enemies are destroyed
    void OnAllEnemiesDestroyed()
    {
        lvlCompleteImage.SetActive(true);
        lvlCompleteText.SetActive(true);
        textTutor.SetActive(false);

        if(playerObject != null)
        {
            cameraController.isClamped = false;
            cameraZoom.StartZoomIn(6f);
        }
    }


}
