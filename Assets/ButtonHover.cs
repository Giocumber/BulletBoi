using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ButtonHover : MonoBehaviour
{
    public GameObject redShadow;

    // Called when the mouse pointer enters the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("das");
        if (redShadow != null)
        {
            redShadow.SetActive(true); // Enable child object
        }
    }

    // Called when the mouse pointer exits the button
    public void OnPointerExit(PointerEventData eventData)
    {
        if (redShadow != null)
        {
            redShadow.SetActive(false); // Disable child object
        }
    }

    public void EnableShadow()
    {
        transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
        redShadow.SetActive(true); // Enable child object
    }

    public void DisableShadow()
    {
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        redShadow.SetActive(false); // Enable child object
    }
}
