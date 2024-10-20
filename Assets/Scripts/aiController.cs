using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AICarFollow : MonoBehaviour
{
    public Transform[] waypoints;  
    public float maxSpeed = 10f;   
    public float turnSpeed = 5f;   
    public float detectionRange = 10f;
    public float brakeDistance = 5f;   

    private int currentWaypointIndex = 0;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            Vector3 targetDirection = waypoints[currentWaypointIndex].position - transform.position;
            float distanceToWaypoint = targetDirection.magnitude;

            // Rotate the car towards the next waypoint
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDirection, turnSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);

            // Adjust the speed of the car
            if (distanceToWaypoint < brakeDistance)
            {
                rb.velocity = transform.forward * (maxSpeed / 2);  // Slow down
            }
            else
            {
                rb.velocity = transform.forward * maxSpeed;        // Full speed
            }

            // Check if AI car reached the current waypoint
            if (distanceToWaypoint < detectionRange)
            {
                currentWaypointIndex++;
            }
        }
        else
        {
            // When all waypoints are reached, stop the car or reset the path
            rb.velocity = Vector3.zero;
        }
    }
}

