using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCarPosition : MonoBehaviour
{
    [SerializeField] private TrackCheckpoints trackCheckpoints; // Reference to TrackCheckpoints
    [SerializeField] private Transform startPoint; // Fallback start point if no checkpoints have been crossed

    private void OnTriggerEnter(Collider other)
    {
        // Check if the car entering the trigger has a valid Transform
        if (other.CompareTag("Car")) // Assuming cars have a "Car" tag
        {
            Transform carTransform = other.transform;

            // Get the car's Rigidbody component to stop forces
            Rigidbody carRigidbody = carTransform.GetComponent<Rigidbody>();

            if (carRigidbody != null)
            {
                // Stop all forces on the car
                carRigidbody.velocity = Vector3.zero; // Stop any forward/backward motion
                carRigidbody.angularVelocity = Vector3.zero; // Stop any rotational movement
                carRigidbody.Sleep(); // Optionally, put the rigidbody to sleep for stability
            }

            // Get the last checkpoint the car crossed
            Transform lastCheckpoint = trackCheckpoints.GetLastCheckpoint(carTransform);

            if (lastCheckpoint != null)
            {
                // Move the car to the last checkpoint's position and reset its rotation
                carTransform.position = lastCheckpoint.position;
                carTransform.rotation = lastCheckpoint.rotation; // Reset orientation as well
                Debug.Log("Car reset to last checkpoint: " + lastCheckpoint.name);
            }
            else
            {
                // No checkpoint crossed, reset to start point
                carTransform.position = startPoint.position;
                carTransform.rotation = startPoint.rotation;
                Debug.Log("Car reset to start point.");
            }
        }
    }
}
