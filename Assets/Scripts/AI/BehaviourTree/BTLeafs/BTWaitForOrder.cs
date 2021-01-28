using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[BTAgent(typeof(BTWaitForOrder))]
public class BTWaitForOrder : BTNode
{
    public override BTResult Execute()
    {
        if (context.contextOwner.IsWaiting()) return BTResult.FAILURE;

        PersonalityTypes customerPersonality = context.customer.GetPersonalityType();
        ChangeInSatisfactionTypes slow;
        ChangeInSatisfactionTypes fast;
        ChangeInSatisfactionTypes failed;

        switch (customerPersonality)
        {
            case PersonalityTypes.ONE:
                slow = ChangeInSatisfactionTypes.MEDIUM;
                fast = ChangeInSatisfactionTypes.NO_CHANGE;
                failed = ChangeInSatisfactionTypes.MEDIUM;

                break;
            case PersonalityTypes.TWO:
                slow = ChangeInSatisfactionTypes.LOW;
                fast = ChangeInSatisfactionTypes.LOW;
                failed = ChangeInSatisfactionTypes.HIGH;

                break;
            case PersonalityTypes.THREE:
                slow = ChangeInSatisfactionTypes.MEDIUM;
                fast = ChangeInSatisfactionTypes.MEDIUM;
                failed = ChangeInSatisfactionTypes.LOW;
                
                break;
            case PersonalityTypes.FOUR:
                slow = ChangeInSatisfactionTypes.LOW;
                fast = ChangeInSatisfactionTypes.HIGH;
                failed = ChangeInSatisfactionTypes.MEDIUM;
                
                break;

            default:
                return BTResult.FAILURE;
                
        }

        context.customer.SetCurrentOutcomesOfAction(
            slow,
            fast,
            failed
        );

        context.contextOwner.Wait();
        
        return BTResult.SUCCESS;
    }
}
