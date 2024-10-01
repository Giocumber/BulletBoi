using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonManager : MonoBehaviour
{
    public RectTransform objTransform;
    private Vector2 startPos;                  // Starting position of the image
    private Vector2 endPos;                    // End position of the image
    public float duration = 0.3f;               // Duration of the slide animation
    private float elapsedTime = 0f;           // Tracks time passed
    private bool isSliding = false;

    [SerializeField] private int slideCounter = 0;
    private int positionPerSlide = -1800;
    private bool canSlide;

    private Button leftBtn;
    private Button rightBtn;

    void Start()
    {
        startPos = new Vector2(0f, 0f);
        endPos = new Vector2(0f, 0f);
        canSlide = true;

        leftBtn = GameObject.Find("LeftButton").GetComponent<Button>();
        rightBtn = GameObject.Find("RightButton").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSliding)
        {
            canSlide = false;
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
                startPos = endPos;
                canSlide = true;
            }
        }

        if (slideCounter == 0)
            leftBtn.gameObject.SetActive(false);
        else
            leftBtn.gameObject.SetActive(true);



        if (slideCounter == 4)
            rightBtn.gameObject.SetActive(false);
        else
            rightBtn.gameObject.SetActive(true);

    }

    public void RightSlide()
    {
        if (canSlide && slideCounter < 4)
        {
            slideCounter++;
            endPos.x = slideCounter * positionPerSlide;
            StartSliding();
        }
    }

    public void LeftSlide()
    {
        if (canSlide && slideCounter > 0)
        {
            slideCounter--;
            endPos.x = slideCounter * positionPerSlide;
            StartSliding();
        }
    }

    public void StartSliding()
    {
        elapsedTime = 0f;  // Reset the time
        isSliding = true;   // Start sliding
    }
}
