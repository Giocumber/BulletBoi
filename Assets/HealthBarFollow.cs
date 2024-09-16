using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    public GameObject hpPos;
    void Update()
    {
        transform.position = hpPos.transform.position;
    }
}
