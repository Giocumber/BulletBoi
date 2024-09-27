using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private static CursorManager instance;

    [SerializeField] private Texture2D cursorTexture;
    private Vector2 cursorHotspot;

    void Awake()
    {
        // Check if an instance of AudioManager already exists
        if (instance == null)
        {
            instance = this; // If not, set this instance
            DontDestroyOnLoad(gameObject); // Make this instance persist across scenes
        }
        else
        {
            Destroy(gameObject); // If an instance already exists, destroy the duplicate
        }
    }


    private void Start()
    {
        cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }
}
