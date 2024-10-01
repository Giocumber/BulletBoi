using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public void CompleteLevel(int currentLevel)
    {
        UpdateUnlockedLevel(currentLevel);
    }

    private void UpdateUnlockedLevel(int currentLevel)
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        if (currentLevel >= unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", currentLevel + 1); // Unlock the next level
            PlayerPrefs.Save(); // Ensure the changes are saved
        }
    }
}
