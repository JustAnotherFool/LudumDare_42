using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour 
{
    [Header("Just link them manually so that it doesn't grab itself too")]
    public Transform[] spawnPoints;
    public GameObject personPrefab;
    int peopleToSpawn = 6;

    void Awake()
    {
        //Knuth shuffle algo
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Transform tempTransform = spawnPoints[i];
            int r = Random.Range(i, spawnPoints.Length);
            spawnPoints[i] = spawnPoints[r];
            spawnPoints[r] = tempTransform;
        }

        for (int i = 0; i < peopleToSpawn; i++) //Note the <= needed because the GetComponentsInChildren in Awake() is also collecting THIS objects transform...
        {
            Instantiate(personPrefab, spawnPoints[i].position, transform.rotation);
        }
    }
}
