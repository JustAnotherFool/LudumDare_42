using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour 
{
    float doorSpeed = 10;
    bool isDeployed;
    public Transform door;
    public GameObject minimapMarker;
    AudioSource audioSource;
    Vector3 target;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        target = door.localPosition;
    }

    public void DeployDoor()
    {
        if (!isDeployed)
        {
            minimapMarker.GetComponent<Renderer>().enabled = false;
            isDeployed = true;
            target = Vector3.zero;
            audioSource.Play();
        }
    }

    void Update()
    {
        if (door.localPosition != target)
        {
            door.localPosition = Vector3.Lerp(door.localPosition, target, Time.deltaTime * doorSpeed);
            float difference = Mathf.Abs(door.localPosition.z - target.z);
            if (difference < 0.025f)
            {
                door.localPosition = new Vector3(0, 0, 0);
            }
        }
    }
}
