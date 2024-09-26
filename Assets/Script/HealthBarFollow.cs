using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    private GameObject hpPos;

    private void Start()
    {
        hpPos = GameObject.Find("HealthPos");
    }

    void Update()
    {
        transform.position = hpPos.transform.position;
    }
}
