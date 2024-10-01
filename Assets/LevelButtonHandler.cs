using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButtonHandler : MonoBehaviour
{
    public Button[] levelButtons; // Array of buttons (assign in the Inspector)

    void Start()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1; // Store the level index (assuming level numbers start at 1)
            levelButtons[i].onClick.AddListener(() => LoadLevel(levelIndex));
        }
    }

    void LoadLevel(int levelNumber)
    {
        //string sceneName = "L" + levelNumber; // Assuming scenes are named "Level1", "Level2", etc.
        SceneManager.LoadScene(levelNumber + 1);
    }
}
