using System;

[BTAgent(typeof(BTHas))]
public class BTHas : BTNode
{
    public HasOp operation;
    public float destinationTolerance = 1;

    public override BTResult Execute()
    {
        BTResult result = BTResult.FAILURE;
        switch (operation)
        {
            case HasOp.PATH:
                result = context.navAgent.hasPath ? BTResult.SUCCESS : BTResult.FAILURE;
                break;
            case HasOp.PATH_TO_TARGET:
                if (!context.navAgent.enabled)
                {
                    result = BTResult.FAILURE;
                }
                else if (context.navAgent.hasPath && context.contextOwner.currentTarget != null)
                {
                    result = PathIsWithinToleranceToTarget() ? BTResult.SUCCESS : BTResult.FAILURE;
                    UnityEngine.Debug.Log("BTHas: PathIsWithinToleranceToTarget = " + result);
                }
                else
                {
                    context.navAgent.ResetPath();
                    result = BTResult.FAILURE;
                }
                break;
            case HasOp.TARGET:
                UnityEngine.Debug.Log("BTHas: currentTarget = " + context.contextOwner.currentTarget);
                result = context.contextOwner.currentTarget != null ? BTResult.SUCCESS : BTResult.FAILURE;
                break;
            case HasOp.STP:
                result = context.activeSmartTerrainPoint != null ? BTResult.SUCCESS : BTResult.FAILURE;
                break;
            case HasOp.SEAT:
                result = context.contextOwner.activeSeat != null ? BTResult.SUCCESS : BTResult.FAILURE;
                break;
        }

        UnityEngine.Debug.Log("BTHas: result = " + result);
        
        return result;
    }

    private bool PathIsWithinToleranceToTarget()
    {
        UnityEngine.Debug.Log("BTHas: sqrMagnitude = " + (
                        context.navAgent.pathEndPosition - context.contextOwner.currentTarget.GetPosition()
                    ).sqrMagnitude);
        return (
                context.navAgent.pathEndPosition - context.contextOwner.currentTarget.GetPosition()
            ).sqrMagnitude < destinationTolerance * destinationTolerance;
    }
}
