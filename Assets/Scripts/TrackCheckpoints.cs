using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpoints : MonoBehaviour
{
    [SerializeField] private List<Transform> carTransformList;
    private List<Checkpoint> checkpointSingleList;
    private List<int> nextCheckpointSingleIndexList;
    private List<Transform> lastCheckpointTransformList;  // To store the last crossed checkpoint for each car

    private void Awake()
    {
        InitializeCheckpoints();
        InitializeCars();
    }

    private void InitializeCheckpoints()
    {
        Transform checkpointsTransform = transform.Find("Checkpoints");
        checkpointSingleList = new List<Checkpoint>();

        foreach (Transform checkpointTransform in checkpointsTransform)
        {
            Checkpoint checkpointSingle = checkpointTransform.GetComponent<Checkpoint>();
            if (checkpointSingle != null)
            {
                checkpointSingle.SetTrackCheckpoints(this);
                checkpointSingleList.Add(checkpointSingle);
            }
            else
            {
                Debug.LogError("Checkpoint component missing on: " + checkpointTransform.name);
            }
        }
    }

    private void InitializeCars()
    {
        nextCheckpointSingleIndexList = new List<int>();
        lastCheckpointTransformList = new List<Transform>();  // Initialize the lastCheckpoint list

        foreach (Transform carTransform in carTransformList)
        {
            nextCheckpointSingleIndexList.Add(0);
            lastCheckpointTransformList.Add(null);  // Initially, no checkpoint has been crossed
        }
        Debug.Log("Car list initialized with " + carTransformList.Count + " cars.");
    }

    public void PlayerThroughCheckpoint(Checkpoint checkpoint, Transform carTransform)
    {
        int carIndex = carTransformList.IndexOf(carTransform);

        if (carIndex == -1)
        {
            Debug.LogError("CarTransform not found in carTransformList!");
            return;
        }

        int nextCheckpointSingleIndex = nextCheckpointSingleIndexList[carIndex];

        if (checkpointSingleList.IndexOf(checkpoint) == nextCheckpointSingleIndex)
        {
            // Correct checkpoint
            Debug.Log("Car " + carIndex + " passed the correct checkpoint.");

            // Update the last crossed checkpoint's transform for this car
            lastCheckpointTransformList[carIndex] = checkpoint.transform;

            // Advance to the next checkpoint
            nextCheckpointSingleIndexList[carIndex] = (nextCheckpointSingleIndex + 1) % checkpointSingleList.Count;
        }
        else
        {
            // Wrong checkpoint
            Debug.LogWarning("Car " + carIndex + " passed the wrong checkpoint.");
        }
    }

    // Method to get the last checkpoint crossed by a car
    public Transform GetLastCheckpoint(Transform carTransform)
    {
        int carIndex = carTransformList.IndexOf(carTransform);

        if (carIndex == -1)
        {
            Debug.LogError("CarTransform not found in carTransformList!");
            return null;
        }

        return lastCheckpointTransformList[carIndex];
    }
}
