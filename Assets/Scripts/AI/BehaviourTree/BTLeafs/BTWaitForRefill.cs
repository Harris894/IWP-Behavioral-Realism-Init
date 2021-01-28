using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[BTAgent(typeof(BTWaitForRefill))]
public class BTWaitForRefill : BTNode
{
    public override BTResult Execute()
    {
        if (context.contextOwner.IsWaiting()) return BTResult.FAILURE;

        PersonalityTypes customerPersonality = context.customer.GetPersonalityType();
        ChangeInSatisfactionTypes slow;
        ChangeInSatisfactionTypes fast;

        switch (customerPersonality)
        {
            case PersonalityTypes.ONE:
            case PersonalityTypes.TWO:
                slow = ChangeInSatisfactionTypes.LOW;
                fast = ChangeInSatisfactionTypes.MEDIUM;

                break;
            case PersonalityTypes.THREE:
                slow = ChangeInSatisfactionTypes.LOW;
                fast = ChangeInSatisfactionTypes.LOW;

                break;
            case PersonalityTypes.FOUR:
                slow = ChangeInSatisfactionTypes.NO_CHANGE;
                fast = ChangeInSatisfactionTypes.LOW;
                
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
