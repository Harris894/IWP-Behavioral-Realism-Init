using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Seat class is 
/// </summary>
public class Seat : MonoBehaviour, IDestination
{
    [Header("Settings")]
    public int maxRange = 10;
    public BehaviourTreeType behaviourTreeType;

    public SeatRuntimeSet RuntimeSet;
    public EatingZone eatingZone;

    internal bool seatTaken = false;

    private void OnEnable()
    {
        RuntimeSet.Add(this);
    }

    private void OnDisable()
    {
        RuntimeSet.Remove(this);
    }

    private void Awake()
    {
        SeatManager.RegisterSeat(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        AIComponent customer = other.GetComponent<AIComponent>();
        if (customer != null && IsAvailable())
        {
            customer.OnSeatReached();
            OnSeatReached();
        
            eatingZone.RegisterOrderManager(customer.GetComponent<OrderingAction>());
            AudioManager.instance.PlayGreetingSound("Hey", other.GetComponent<AudioSource>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        AIComponent customer = other.GetComponent<AIComponent>();
        if (customer != null && !IsAvailable())
        {
            SeatManager.OnSeatExit(this);
            customer.CustomerLeaves();
            OnExit();
        
            eatingZone.UnregisterOrderManager();
        }
    }

    public bool IsAvailable() { return !seatTaken; }

    public void OnSeatReached()
    {
        seatTaken = true;
    }

    public void OnExit()
    {
        seatTaken = false;
    }

    /// <summary>
    /// Get the current position of the seat.
    /// </summary>
    /// <returns>The current Vector3 position of the seat</returns>
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void OnDestinationReached()
    {
        UnityEngine.Debug.Log("Seat Taken: " + seatTaken);
        
        // OnSeatReached();
    }
}
