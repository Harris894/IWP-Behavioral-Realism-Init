using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class BTContext
{
    public AIComponent contextOwner;
    public OrderingAction orderManager;
    public Customer customer;
    public Animator animatorController;
    public NavMeshAgent navAgent;

    public SmartTerrainPoint activeSmartTerrainPoint;
    public Seat activeSeat;

#if UNITY_EDITOR
    public List<string> behaviourHistory = new List<string>();
#endif //UNITY EDITOR

    public BTContext(AIComponent _owner, Customer _customer, OrderingAction _orderManager, Animator _animatorController, NavMeshAgent _navAgent)
    {
        contextOwner = _owner;
        customer = _customer;
        orderManager = _orderManager;
        animatorController = _animatorController;
        navAgent = _navAgent;
    }
}
