using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// The Entrance class is 
/// </summary>
public class Entrance : Singleton<Entrance>, IDestination
{
    [Header("Settings")]
    public BehaviourTreeType behaviourTreeType;

    private void Start()
    {   
    }

    private void Update()
    {   
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }

    public void OnEntranceReached()
    {
    }

    /// <summary>
    /// Get the current position of the entrance.
    /// </summary>
    /// <returns>The current Vector3 position of the entrance</returns>
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void OnDestinationReached()
    {
        OnEntranceReached();
    }
}
