using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemies : MonoBehaviour
{
    public bool allEnemiesDestroyed = false;  // Tracks whether all enemies are destroyed
    public GameObject lvlCompleteImage;
    public GameObject lvlCompleteText;
    public GameObject textTutor;

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
        Debug.Log("All enemies are destroyed!");
        lvlCompleteImage.SetActive(true);
        lvlCompleteText.SetActive(true);
        textTutor.SetActive(false);
    }
}
