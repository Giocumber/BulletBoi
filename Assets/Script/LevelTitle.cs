using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTitle : MonoBehaviour
{
    private GameObject textObject;
    // Start is called before the first frame update
    void Start()
    {
        textObject = GameObject.Find("TextTutor");
        textObject.SetActive(true);
        Invoke("DisableText", 3f);
    }

    void DisableText()
    {
        textObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
