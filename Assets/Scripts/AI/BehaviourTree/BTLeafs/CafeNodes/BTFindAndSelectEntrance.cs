using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;

[BTEntrance(typeof(BTFindAndSelectEntrance))]
public class BTFindAndSelectEntrance : BTNode
{
    public override BTResult Execute()
    {
        BTResult result = BTResult.FAILURE;

        Type entranceType = typeof(Entrance);
        if (context.contextOwner.currentTarget != null && 
            context.contextOwner.currentTarget.GetType().Equals(entranceType))
        {
            result = BTResult.SUCCESS;
        }

        Entrance entrance = EntranceManager.SelectEntrance(context.contextOwner, out NavMeshPath _path);
        if (entrance != null)
        {
            context.contextOwner.currentTarget = entrance;

            context.navAgent.isStopped = true;
            context.navAgent.ResetPath();
            context.navAgent.SetPath(_path);

            result = BTResult.SUCCESS;
        }
        else
        {
            result = BTResult.FAILURE;
        }

        return result;
    }
}
