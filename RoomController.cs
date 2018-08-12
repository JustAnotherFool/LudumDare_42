using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomController : MonoBehaviour 
{
    [Header("NOTE: Saferoom needs a copy too")]
    public bool isSaferoom;
    public bool isAlwaysEligble;

    [Header("Linkables")]
    public Light lightRed;
    public Light lightWhite;
    public GameObject minimapMarker;
    public RoomController[] connectedRooms;
    public DoorController[] doorControllers;

    [HideInInspector]
    public bool isEligble;
    bool isExploded;
    bool isPlayerPresent;

    void Awake()
    {
        if (!isSaferoom)
        {
            CountConnectedRooms();
        }
    }

    public void CountConnectedRooms()
    {
        if (isAlwaysEligble && !isExploded) //Override for the room in the back
        {
            isEligble = true;
        }
        else if (!isExploded)
        {
            int count = 0;
            for (int i = 0; i < connectedRooms.Length; i++)
            {
                if (connectedRooms[i].isExploded == false) //Count unexploded rooms connected
                {
                    count++;
                } 
            }
            
            if (count == 1)
            {
                isEligble = true;
            }
        }
    }

    public void Explode()
    {
        isExploded = true;
        isEligble = false;
        StartCoroutine(Exploding());
    }

    IEnumerator Exploding()
    {
        lightWhite.enabled = false;
        lightRed.enabled = true;
        minimapMarker.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(12); //Time it takes before sealing
        minimapMarker.GetComponent<MeshRenderer>().enabled = false;
        lightRed.intensity = 0;
        //lightRed.enabled = false; //FOR SOME REASON THIS REFUSES TO WORK. IGNORING IT FOR NOW.

        for (int i = 0; i < doorControllers.Length; i++)
        {
            doorControllers[i].DeployDoor();
        }

        //If player is trapped in a closed room.
        yield return new WaitForSeconds(2f); //So the player can realize their mistake before loading game over screen
        if (isPlayerPresent)
        {
            SceneManager.LoadScene(2);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            isPlayerPresent = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            isPlayerPresent = false;
        }
    }
}
