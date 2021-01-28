using System;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class SensorySystem
{
    public float targetLostMaxTime;
    public float fovAngle = 60;
    public float fovDistance = 10;

    internal Vector3 lastKnownPosition = Vector3.zero;

    NavMeshAgent navAgent;
    AIComponent aiComponent;

    float targetLostTimer = 0;

    public void Initialize(AIComponent _aiComponent, NavMeshAgent _navAgent)
    {
    }

    internal void Update()
    {
    }

    private void UpdateHasTarget()
    {
    }

    public bool IsEventSourceVisible(IEventSource _source)
    {
        return true;
    }
}
