using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaferoomController : MonoBehaviour 
{
    public DeadCount deadCount;
    int totalPeople;
    int savedPeople;


    void Start()
    {
        //Not the best way to automatically count people, but my brain is failing me and time is short. This works.

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Person")
        {
            collider.GetComponent<PersonController>().isSafe = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Person")
        {
            collider.GetComponent<PersonController>().isSafe = false;
        }
    }

    public void Endgame()
    {       
        GameObject[] people = GameObject.FindGameObjectsWithTag("Person");
        totalPeople = people.Length;
        for (int i = 0; i < totalPeople; i++)
        {
            if (people[i].GetComponent<PersonController>().isSafe)
            {
                savedPeople++;
            }
        }

        int lostPeople = totalPeople - savedPeople; //Return this value or use it to vizualise. Instead of showing lives saved, show lives lost because it's funny.
        if (lostPeople == 0)
        {
            SceneManager.LoadScene(4); //Load win screen. (As if...)
        }
        else
        {
            //Load up the "messenger" that carries the value to the next scene
            DontDestroyOnLoad(deadCount.gameObject); 
            deadCount.deadCount = lostPeople;
            SceneManager.LoadScene(3); //Load "win" screen.
        }
    }
}
