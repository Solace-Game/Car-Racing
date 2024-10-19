using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramp : MonoBehaviour
{
    public float boostForce = 500f;  // Amount of force to apply along the x-axis

    // Ensure that the Box Collider is set as a trigger
    void Start()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            boxCollider.isTrigger = true;  // Make sure the collider is a trigger
        }
    }

    // This function is called when another object enters the trigger
    void OnTriggerEnter(Collider other)
    {
        // Check if the object has a Rigidbody component (i.e., it's the car or something that can move)
        Rigidbody carRigidbody = other.GetComponent<Rigidbody>();
        if (carRigidbody != null)
        {
            // Apply an instantaneous force to the car's x-axis
            Vector3 forceDirection = new Vector3(boostForce, 0, 0);  // Apply force along the x-axis
            carRigidbody.AddForce(forceDirection, ForceMode.Impulse);  // Apply force instantly
        }
    }
}

