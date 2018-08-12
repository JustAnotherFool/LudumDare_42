using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonController : MonoBehaviour 
{
    [Header("Linkables")]
    public Transform leftArm;
    public Transform rightArm;
    public Transform bodyMesh;

    [Header("Files")]
    public AudioClip audioYelp1;
    public AudioClip audioYelp2;

    [Header("Settings")]
    public bool isFloater;

    [HideInInspector]
    public bool isSafe;

    //Internals

    float randomX;
    float randomY;
    float randomZ;

    bool isGrabbed;
    float recoverySpeed = 2;
    float wobbleSpeed = 40;
    float wobbleAmount = 25;
    float moveSpeed = 0.01f;
    float rotation = 1;

    AudioSource audioSource;

    Rigidbody rb;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DirectionTimer());

        if (isFloater) //Makes them float helplessly in space for menu screens
        {
            isGrabbed = true;
            rb.isKinematic = true;
            float max = 0.75f;
            float min = 0.05f;
            randomX = Random.Range(min,max);
            randomY = Random.Range(min,max);
            randomZ = Random.Range(min,max);
            bodyMesh.localPosition = new Vector3(bodyMesh.localPosition.x, 0.1f,bodyMesh.localPosition.z); //Moves the pivot so they rotate properly in space
        }
    }

    public void GrabToggle()
    {
        if (!isGrabbed) 
        {
            audioSource.clip = audioYelp1;
            isSafe = false; //Fixes the problem where the disabled collider doesn't trigger OnTriggerExit 
        }
        else
        {
            audioSource.clip = audioYelp2;
        }
        audioSource.Play();
        isGrabbed = !isGrabbed; //Inverts the bool
    }

    void Update()
    {
        //Arm flailing
        leftArm.localRotation = Quaternion.Euler(leftArm.localEulerAngles.x, Mathf.Sin(Time.realtimeSinceStartup * wobbleSpeed) * wobbleAmount + 180, leftArm.localEulerAngles.z);
        rightArm.localRotation = Quaternion.Euler(rightArm.localEulerAngles.x, Mathf.Sin(Time.realtimeSinceStartup * wobbleSpeed) * wobbleAmount + 180, rightArm.localEulerAngles.z);

        if (!isGrabbed)
        {
            //Makes Person upright over time
            Quaternion rot = Quaternion.FromToRotation(transform.up, Vector3.up);
            rb.AddTorque(new Vector3(rot.x, rot.y, rot.z)* recoverySpeed);
            
            //Run around randomly
            rb.AddForce(transform.forward * moveSpeed, ForceMode.VelocityChange);
            rb.AddTorque(transform.up * rotation * 0.2f);
        }

        if (isFloater)
        {
            transform.Rotate(randomX,randomY,randomZ);
        }
    }

    IEnumerator DirectionTimer() //Randomises rotation amount 
    {
        yield return new WaitForSeconds(Random.Range(0f,2f));
        rotation = Random.Range(0f,1f);
        if (Random.Range(0,2) == 0)
        {
            rotation = -rotation;
        }
        StartCoroutine(DirectionTimer());
    }
}
