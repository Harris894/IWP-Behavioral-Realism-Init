using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class EntranceManager
{
    public static Entrance SelectEntrance(AIComponent _agent, out NavMeshPath _path)
    {
        Entrance entrance = Entrance.GetInstance();
        _path = null;

        if (IsEntranceValid(entrance, _agent, out _path)) 
        {
            return entrance;
        }

        return null;
    }

    static bool IsEntranceValid(Entrance _entrance, AIComponent _agent, out NavMeshPath _path) 
    {
        bool isValid = false;
        _path = null;
        
        if (_agent.behaviourTreeType == _entrance.behaviourTreeType)
        {
            if (IsOnNavmesh(_entrance, out NavMeshHit _hit))
            {
                _path = new NavMeshPath();
                NavMesh.CalculatePath(_agent.GetPosition(), _hit.position, NavMesh.AllAreas, _path);

                isValid = _path.status == NavMeshPathStatus.PathComplete;
            }
        }

        return isValid;
    }

    static bool IsOnNavmesh(Entrance _entrance, out NavMeshHit _hit)
    {
        _hit = default;

        bool isOnNavmesh = true;
        if (!NavMesh.SamplePosition(_entrance.transform.position, out _hit, 10, NavMesh.AllAreas))
        {
            isOnNavmesh = false;
        }

        return isOnNavmesh;
    }
}
