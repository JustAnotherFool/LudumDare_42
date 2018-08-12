using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour 
{
    Transform targetTransform;
    Transform heldTransform;
    SphereCollider triggerCollider;

    void Awake()
    {
        triggerCollider =  GetComponent<SphereCollider>();
    }

    public void GrabToggle()
    {
        if (heldTransform == null && targetTransform != null)
        {
            triggerCollider.isTrigger = false; //Turns the trigger into a proper collider to avoid clipping carried objects into walls
            heldTransform = targetTransform; //This is so that he doesn't accidentally snag another object while moving.
            if (heldTransform.tag == "Person")
            {
                heldTransform.GetComponent<PersonController>().GrabToggle();
            }
            
            foreach(Collider c in heldTransform.GetComponents<Collider>()) 
            {
                c.enabled = false;
            }

            heldTransform.GetComponent<Rigidbody>().isKinematic = true;
            heldTransform.position = transform.position;
            heldTransform.parent = transform;

        }
        else if (heldTransform != null)
        {
            triggerCollider.isTrigger = true;
            if (heldTransform.tag == "Person")
            {
                heldTransform.GetComponent<PersonController>().GrabToggle();
            }
            heldTransform.GetComponent<Rigidbody>().isKinematic = false;
            foreach(Collider c in heldTransform.GetComponents<Collider>()) 
            {
                c.enabled = true;
            }
            heldTransform.parent = null;
            heldTransform = null;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Grabbable" || collider.tag == "Person")
        {
            targetTransform = collider.transform;
        }
    }

    void OnTriggerExit()
    {
        targetTransform = null;
    }
}
