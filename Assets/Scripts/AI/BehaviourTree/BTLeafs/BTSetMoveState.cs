using System;
using UnityEngine;
using UnityEngine.AI;

[BTAgent(typeof(BTSetMoveState))]
public class BTSetMoveState : BTNode
{
    public MovementState desiredMoveState;
    public override BTResult Execute()
    {
        BTResult result = BTResult.SUCCESS;
        
        Debug.Log("BTSetMoveState: contextOwner.currentMoveState = " + context.contextOwner.currentMoveState + "; desiredMoveState = " + desiredMoveState);
        if (context.contextOwner.currentMoveState == desiredMoveState) return result;

        string animatorParameter = MovementState.WALK.ToString().ToLower();
        switch (desiredMoveState)
        {
            case MovementState.IDLE:
                context.animatorController.SetBool(animatorParameter, false);
                break;
            case MovementState.WALK:
                context.animatorController.SetBool(animatorParameter, true);
                break;
            default:
                context.animatorController.SetBool(animatorParameter, false);
                break;
        }

        context.contextOwner.currentMoveState = desiredMoveState;

        return result;
    }
}
