using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButtonHandler : MonoBehaviour
{
    public Button[] levelButtons; // Array of buttons (assign in the Inspector)

    void Start()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1; // Assuming level numbers start at 1
            Transform lockIcon = levelButtons[i].transform.Find("Image"); //papalitan sa sunod ang name
            Transform levelNumberText = levelButtons[i].transform.Find("Text (TMP)"); //papalitan sa sunod ang name
            Image buttonImage = levelButtons[i].GetComponent<Image>(); // Get the Image component of the button

            // If the level is unlocked, enable the button and add the listener
            if (levelIndex <= unlockedLevel)
            {
                levelButtons[i].interactable = true;
                int levelNumber = levelIndex; // Store index for use in listener
                levelButtons[i].onClick.AddListener(() => LoadLevel(levelNumber));
                lockIcon.gameObject.SetActive(false);
                levelNumberText.gameObject.SetActive(true);
            }
            else
            {
                buttonImage.raycastTarget = false; // Prevent clicks on the button
                lockIcon.gameObject.SetActive(true);
            }
        }
    }

    void LoadLevel(int levelNumber)
    {
        //string sceneName = "L" + levelNumber; // Assuming scenes are named "Level1", "Level2", etc.
        SceneManager.LoadScene(levelNumber + 1);
    }
}
