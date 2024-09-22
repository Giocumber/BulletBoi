using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    public float slowMotionFactor = 0.2f; // 20% of normal speed
    public bool slowMotionEffect = false; // To track slow-motion state
    private bool isSlowMoActive = false;


    public CameraZoom cameraZoom;
    private GameObject otherObj;
    private CheckEnemies checkEnemies;

    private SceneManagerScript sceneManagerScript;


    private void Start()
    {
        otherObj = GameObject.Find("UI_Manager");
        if (otherObj != null)
           checkEnemies = otherObj.GetComponent<CheckEnemies>();

        otherObj = GameObject.Find("SceneManager");
        if (otherObj != null)
            sceneManagerScript = otherObj.GetComponent<SceneManagerScript>();
    }


    void Update()
    {
        // Check if the space key is being held down
        if (Input.GetKey(KeyCode.Space) && !checkEnemies.allEnemiesDestroyed && !sceneManagerScript.isPaused)
        {
            ActivateSlowMotion();
        }
        else
        {
            DeactivateSlowMotion();
            cameraZoom.ReturnToOriginalZoom();
        }
    }

    void ActivateSlowMotion()
    {
        // Apply slow-motion effect if it's not already active
        if (!isSlowMoActive)
        {
            isSlowMoActive = true;
            Time.timeScale = slowMotionFactor; // Slow down the game
            Time.fixedDeltaTime = Time.timeScale * 0.02f; // Adjust physics calculations
            slowMotionEffect = true; // Mark the slow-motion effect as active
        }
    }

    void DeactivateSlowMotion()
    {

        // Revert to normal time scale if slow-motion is active
        if (isSlowMoActive && !sceneManagerScript.isPaused)
        {
            isSlowMoActive = false;
            Time.timeScale = 1f; // Reset to normal speed
            Time.fixedDeltaTime = 0.02f; // Reset physics step time
            slowMotionEffect = false; // Mark slow-motion effect as inactive
        }
    }
}
