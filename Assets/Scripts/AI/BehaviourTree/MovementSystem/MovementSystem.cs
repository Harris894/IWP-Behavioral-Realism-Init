using System;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class MovementSystem
{    
    internal Vector3 lastKnownPosition = Vector3.zero;

    NavMeshAgent navAgent;
    AIComponent aiComponent;

    public void Initialize(AIComponent _aiComponent, NavMeshAgent _navAgent)
    {
        aiComponent = _aiComponent;
        navAgent = _navAgent;
    }

    internal void Update()
    {
    }

    internal void GoToBartender()
    {
        
    }
}
