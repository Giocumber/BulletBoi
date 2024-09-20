using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Camera cam;  // Reference to the camera
    public float targetZoom = 3f;  // Zoom level you want to reach
    public float zoomSpeed = 2f;  // Speed of zooming in/out
    public float returnDelay = 2f;  // Delay before returning to the original zoom
    public float zoomLerpSpeed = 10f;  // Smoothing factor for zoom transitions

    private float originalZoom;  // Original zoom level of the camera
    private bool zoomingIn = false;

    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;  // Default to the main camera if not assigned
        }

        // Store the camera's initial orthographic size
        originalZoom = cam.orthographicSize;
    }

    void Update()
    {
        // Zoom in smoothly if triggered
        if (zoomingIn)
        {
            // Interpolate the zoom value to the target zoom
            float newZoom = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
            cam.orthographicSize = newZoom;
        }
        else
        {
            float newZoom = Mathf.Lerp(cam.orthographicSize, originalZoom, Time.deltaTime * zoomLerpSpeed);
            cam.orthographicSize = newZoom;

            // Stop returning once we're close to the original zoom
            if (Mathf.Abs(cam.orthographicSize - originalZoom) < 0.01f)
            {
                cam.orthographicSize = originalZoom;  // Ensure it's set exactly to the original zoom
            }
        }
    }

    // Call this function to trigger zooming in
    public void StartZoomIn(float newTargetZoom)
    {
        targetZoom = newTargetZoom;
        zoomingIn = true;
    }

    // Return to the original zoom level
    public void ReturnToOriginalZoom()
    {
        zoomingIn = false;
    }
}
