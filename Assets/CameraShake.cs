using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform cameraTransform; // Reference to the camera's transform
    public float shakeDuration = 0.5f; // Duration of the shake
    public float shakeMagnitude = 0.1f; // Magnitude of the shake

    private Vector3 shakeOffset = Vector3.zero; // Offset added by the shake
    private bool isShaking = false;

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform; // Use the main camera if no other camera is assigned
        }
    }

    public void TriggerShake()
    {
        if (!isShaking)
        {
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake()
    {
        isShaking = true;
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            // Generate random offset for the shake
            shakeOffset = Random.insideUnitSphere * shakeMagnitude;
            shakeOffset.z = 0f; // Keep the Z axis unchanged

            elapsed += Time.deltaTime;

            yield return null; // Wait for the next frame
        }

        shakeOffset = Vector3.zero; // Reset shake offset after shake ends
        isShaking = false;
    }

    public Vector3 GetShakeOffset()
    {
        return shakeOffset;
    }
}
