using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour 
{
    public SaferoomController saferoomController;

    void Start()
    {
        Debug.Log("DISABLE THIS IMMEDIATELY AFTER TESTING");
        StartCoroutine(Go()); //Ends the game.
    }

    IEnumerator Go()
    {
        yield return new WaitForSeconds(1f); //Delay so all colliders/triggers can register properly
        saferoomController.Endgame();
    }
}
