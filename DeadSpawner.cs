using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSpawner : MonoBehaviour 
{
    [Header("Linkables")]
    public GameObject personPrefab;


    //Internals
    public Transform[] spawnPoints;
    int deadCount;
    DeadCount dc;
    
    void Start()
    {
        //This whole thing gets kinda messy. Sorry.
        dc = GameObject.Find("DeadCount").GetComponent<DeadCount>();
        SpawnDead(dc.deadCount); 
        Destroy(dc.gameObject);
    }

    public void SpawnDead(int count)
    {
        for (int i = 0; i < count; i++) //Note the <= needed because the GetComponentsInChildren in Awake() is also collecting THIS objects transform...
        {
            GameObject person = Instantiate(personPrefab, spawnPoints[i].position, transform.rotation);
            person.GetComponent<PersonController>().isFloater = true;
        }
    }
}
