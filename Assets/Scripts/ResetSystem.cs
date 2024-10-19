using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSystem : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private TrackCheckpoints trackCheckpoints; // Reference to TrackCheckpoints
    [SerializeField] private Transform startPoint; // Fallback start point if no checkpoints have been crossed

    void Start()
    {
        // Get the Rigidbody component attached to this GameObject
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (rb.velocity.y <= -15 || Input.GetKeyDown(KeyCode.R)){
            Reset();
        }
    }

    void Reset(){
        Debug.Log("Resetting Car");
        rb.velocity = Vector3.zero; // Stop any forward/backward motion
        rb.angularVelocity = Vector3.zero; // Stop any rotational movement
        rb.Sleep(); // Optionally, put the rigidbody to sleep for stability
        Transform lastCheckpoint = trackCheckpoints.GetLastCheckpoint(rb.transform);
        if (lastCheckpoint != null)
        {
            // Move the car to the last checkpoint's position and reset its rotation
            rb.position = lastCheckpoint.position;
            // rb.rotation = lastCheckpoint.rotation; // Reset orientation as well
            Debug.Log("Car reset to last checkpoint: " + lastCheckpoint.name);
        }
        else
        {
            // No checkpoint crossed, reset to start point
            rb.position = startPoint.position;
            rb.rotation = startPoint.rotation;
            Debug.Log("Car reset to start point.");
        }
    }
}
