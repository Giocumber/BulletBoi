using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingUI : MonoBehaviour
{
    public RectTransform objTransform;  // Reference to the Image's RectTransform
    public Vector2 startPos;                  // Starting position of the image
    public Vector2 endPos;                    // End position of the image
    public float duration = 2f;               // Duration of the slide animation
    private float elapsedTime = 0f;           // Tracks time passed

    private bool isSliding = false;

    void Awake()
    {
        Invoke("StartSliding", 0.5f);
    }

    void Start()
    {
        // Set the initial position to the starting point
        objTransform.anchoredPosition = startPos;
    }

    void Update()
    {
        if (isSliding)
        {
            // Increase the elapsed time by the time since the last frame
            elapsedTime += Time.deltaTime;

            // Calculate the normalized time (between 0 and 1)
            float t = elapsedTime / duration;
            t = Mathf.Clamp01(t);  // Ensure it doesn't go above 1

            // Smoothly interpolate the position from start to end using Lerp
            objTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, t);

            // Stop the sliding if the animation is complete
            if (t >= 1f)
            {
                isSliding = false;
            }
        }
    }

    // Call this method to start the sliding animation
    public void StartSliding()
    {
        elapsedTime = 0f;  // Reset the time
        isSliding = true;   // Start sliding
    }
}
