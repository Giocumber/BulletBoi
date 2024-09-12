using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    public float slowMotionFactor = 0.2f; // 20% of normal speed
    public bool slowMotionEffect = false; // To track slow-motion state
    private bool isSlowMoActive = false;

    void Update()
    {
        // Check if the space key is being held down
        if (Input.GetKey(KeyCode.Space))
        {
            ActivateSlowMotion();
        }
        else
        {
            DeactivateSlowMotion();
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
        if (isSlowMoActive)
        {
            isSlowMoActive = false;
            Time.timeScale = 1f; // Reset to normal speed
            Time.fixedDeltaTime = 0.02f; // Reset physics step time
            slowMotionEffect = false; // Mark slow-motion effect as inactive
        }
    }

    //IEnumerator ActivateSlowMotion()
    //{
    //    isSlowMoActive = true;

    //    // Set time scale to the slow-motion factor
    //    Time.timeScale = slowMotionFactor;
    //    Time.fixedDeltaTime = Time.timeScale * 0.02f; // Adjust the fixedDeltaTime accordingly

    //    // Wait for the duration of the slow-motion effect
    //    yield return new WaitForSecondsRealtime(slowMotionDuration);

    //    // Reset time scale back to normal
    //    Time.timeScale = 1f;
    //    Time.fixedDeltaTime = 0.02f; // Reset the fixedDeltaTime back to the default

    //    isSlowMoActive = false;
    //}



    //IEnumerator GradualSlowMotion()
    //{
    //    isSlowMoActive = true;

    //    float currentTime = 0f;
    //    float startScale = Time.timeScale;
    //    float targetScale = slowMotionFactor;

    //    while (currentTime < 1f)
    //    {
    //        currentTime += Time.unscaledDeltaTime;
    //        Time.timeScale = Mathf.Lerp(startScale, targetScale, currentTime);
    //        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    //        yield return null;
    //    }

    //    // Stay in slow motion for the duration
    //    yield return new WaitForSecondsRealtime(slowMotionDuration);

    //    // Gradually return to normal speed
    //    currentTime = 0f;
    //    while (currentTime < 1f)
    //    {
    //        currentTime += Time.unscaledDeltaTime;
    //        Time.timeScale = Mathf.Lerp(targetScale, 1f, currentTime);
    //        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    //        yield return null;
    //    }

    //    isSlowMoActive = false;
    //}
}
