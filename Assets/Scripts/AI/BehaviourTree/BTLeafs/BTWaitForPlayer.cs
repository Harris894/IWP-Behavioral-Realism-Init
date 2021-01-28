using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[BTAgent(typeof(BTWaitForPlayer))]
public class BTWaitForPlayer : BTNode
{
    public override BTResult Execute()
    {
        UnityEngine.Debug.Log("BTWaitForPlayer: IsWaiting = " + context.contextOwner.IsWaiting());
        if (context.contextOwner.IsWaiting()) return BTResult.FAILURE;

        PersonalityTypes customerPersonality = context.customer.GetPersonalityType();
        ChangeInSatisfactionTypes slow;
        ChangeInSatisfactionTypes fast;

        switch (customerPersonality)
        {
            case PersonalityTypes.ONE:
                slow = ChangeInSatisfactionTypes.MEDIUM;
                fast = ChangeInSatisfactionTypes.NO_CHANGE;

                context.contextOwner.CallPlayer();

                break;
            case PersonalityTypes.TWO:
                slow = ChangeInSatisfactionTypes.MEDIUM;
                fast = ChangeInSatisfactionTypes.LOW;

                break;
            case PersonalityTypes.THREE:
                slow = ChangeInSatisfactionTypes.LOW;
                fast = ChangeInSatisfactionTypes.NO_CHANGE;

                context.contextOwner.CallPlayer();
                
                break;
            case PersonalityTypes.FOUR:
                slow = ChangeInSatisfactionTypes.LOW;
                fast = ChangeInSatisfactionTypes.NO_CHANGE;
                
                break;
            default:
                return BTResult.FAILURE;

        }

        context.customer.SetCurrentOutcomesOfAction(
            slow,
            fast,
            ChangeInSatisfactionTypes.NO_CHANGE
        );

        context.contextOwner.Wait();

        return BTResult.SUCCESS;
    }
}
