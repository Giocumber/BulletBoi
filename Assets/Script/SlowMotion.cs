using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    public float slowMotionFactor = 0.2f; // 20% of normal speed
    public bool slowMotionEffect = false; // To track slow-motion state
    private bool isSlowMoActive = false;

    private CameraZoom cameraZoom;
    private CheckEnemies checkEnemies;
    private SceneManagerScript sceneManagerScript;
    private PlayerScript playerScript;

    private void Start()
    {
        cameraZoom = GameObject.Find("MainCamera").GetComponent<CameraZoom>();
        checkEnemies = GameObject.Find("UI_Manager").GetComponent<CheckEnemies>();
        sceneManagerScript = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
        playerScript = GetComponent<PlayerScript>();
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

        if (isSlowMoActive)
            playerScript.bulletAttackCD = 0.05f;
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

    public void DeactivateSlowMotion()
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
