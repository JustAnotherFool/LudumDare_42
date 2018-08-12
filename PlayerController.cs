using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    [Header("Linkables")]
    public GrabController grabController;

    //Internals
    float moveSpeed = 0.35f;
    float rotateSpeed = 6f;
    bool isMovingForward;
    bool isMovingBackward;
    bool isRotatingLeft;
    bool isRotatingRight;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) //Forward Motion
        {
            isMovingForward = true;    
        }
        else
        {
            if (isMovingForward)
            {
                isMovingForward = false;
            }
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) //Backward Motion
        {
            isMovingBackward = true;
        }
        else
        {
            if (isMovingBackward)
            {
                isMovingBackward = false;
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) //Rotate Left
        {
            isRotatingLeft = true;
        }
        else
        {
            if (isRotatingLeft)
            {
                isRotatingLeft = false;
            }
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) //Rotate Right
        {
            isRotatingRight = true;
        }
        else
        {
            if (isRotatingRight)
            {
                isRotatingRight = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            grabController.GrabToggle();
        }
    }

    void FixedUpdate()
    {
        if (isMovingForward)
        {
            rb.AddForce(transform.forward * moveSpeed ,ForceMode.VelocityChange);
        }
        else if (isMovingBackward)
        {
            rb.AddForce(transform.forward * (-moveSpeed * 0.25f) ,ForceMode.VelocityChange);
        }

        if (isRotatingLeft)
        {
            rb.AddTorque(transform.up * -rotateSpeed);
        }
        else if (isRotatingRight)
        {
            rb.AddTorque(transform.up * rotateSpeed);
        }
    }

    void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0,transform.localEulerAngles.y,0);  //Freeze rotation doesn't always work properly, so this is a failsafe.
    }
}
