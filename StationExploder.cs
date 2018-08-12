using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationExploder : MonoBehaviour 
{
    public GameObject alarm;
    public SaferoomController saferoomController;
    public RoomController[] rooms;
    RoomController targetRoom;
    int exploded;
	
    void Awake()
    {
        rooms = GetComponentsInChildren<RoomController>(); //Collect all rooms

        // Knuth shuffle algorithm - Magically shuffles array contents
        for (int i = 0; i < rooms.Length; i++)
        {
            RoomController tempRoomControllerContainer = rooms[i];
            int r = Random.Range(i, rooms.Length);
            rooms[i] = rooms[r];
            rooms[r] = tempRoomControllerContainer;
        }
        StartCoroutine(ExplodeTimer()); //Start Loop
    }

    IEnumerator ExplodeTimer()
    {
        exploded++;
        if (exploded > rooms.Length)
        {
            yield return new WaitForSeconds(3f);
            saferoomController.Endgame();            
        }

        for (int i = 0; i < rooms.Length; i++) //Refresh connected rooms after last room removal
        {
            rooms[i].CountConnectedRooms();
        }

        yield return new WaitForSeconds(1f); //Delay between rooms
        FindFirstEligble();
        targetRoom.Explode();
        alarm.transform.position = targetRoom.transform.position;
        alarm.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(12f); //Wait until room is done 
        alarm.GetComponent<AudioSource>().Stop();
        StartCoroutine(ExplodeTimer()); //Loop
    }

    void FindFirstEligble() //Find first eligble room in an already randomized array. If no eligble rooms are found, end the game.
    {
        for (int i = 0; i < rooms.Length; i++)
        {
            if (rooms[i].isEligble)
            {
                targetRoom = rooms[i];
                break;
            }
        }
    }
}