using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotator : MonoBehaviour 
{
    void Update()
    {
        transform.Rotate(Vector3.up * 2);
    }
}
