using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class SeatManager
{
    static List<Seat> availableSeatsList = new List<Seat>();
    static List<Seat> SeatsInUseList = new List<Seat>();

    public static void RegisterSeat(Seat _seat) 
    {
        if (IsOnNavmesh(_seat))
        {
            availableSeatsList.Add(_seat);
        }
        else UnityEngine.Debug.LogWarningFormat("Seat {0} is not on navmesh and will be ignored", _seat.name);
    }

    public static Seat SelectNearestReachableSeat(AIComponent _agent, out NavMeshPath _path)
    {
        Seat validSeat = null;
        _path = null;

        availableSeatsList.Sort
            ((x, y) => (x.transform.position - _agent.transform.position).sqrMagnitude.
            CompareTo((y.transform.position - _agent.transform.position).sqrMagnitude));

        foreach (Seat _seat in availableSeatsList) 
        {
            if (IsSeatValid(_seat, _agent, out _path)) 
            {
                validSeat = _seat;
                break;
            }
        }

        if (validSeat != null) 
        {
            availableSeatsList.Remove(validSeat);
            SeatsInUseList.Add(validSeat);
        }

        return validSeat;
    }

    static bool IsSeatValid(Seat _seat, AIComponent _agent, out NavMeshPath _path) 
    {
        bool isValid = false;
        _path = null;
        
        if (_agent.behaviourTreeType == _seat.behaviourTreeType)
        {
            Vector3 distance = _seat.transform.position - _agent.GetPosition();
            if (distance.sqrMagnitude <= _seat.maxRange * _seat.maxRange)
            {
                if (IsOnNavmesh(_seat, out NavMeshHit _hit))
                {
                    _path = new NavMeshPath();
                    NavMesh.CalculatePath(_agent.GetPosition(), _hit.position, NavMesh.AllAreas, _path);

                    isValid = _path.status == NavMeshPathStatus.PathComplete;
                }
            }
        }

        return isValid;
    }

    static bool IsOnNavmesh(Seat _seat, out NavMeshHit _hit)
    {
        _hit = default;

        bool isOnNavmesh = true;
        if (!NavMesh.SamplePosition(_seat.transform.position, out _hit, 10, NavMesh.AllAreas))
        {
            isOnNavmesh = false;
        }

        return isOnNavmesh;
    }

    static bool IsOnNavmesh(Seat _seat) 
    {
        return IsOnNavmesh(_seat, out NavMeshHit _hit);
    }

    public static void OnSeatExit(Seat _seat) 
    {
        if (SeatsInUseList.Contains(_seat)) 
        {
            SeatsInUseList.Remove(_seat);
            availableSeatsList.Add(_seat);
        }
    }
}
