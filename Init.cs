using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour 
{
    void Awake()
    {
         Screen.SetResolution(800, 800, false);
         Application.targetFrameRate = 60;
    }
}
