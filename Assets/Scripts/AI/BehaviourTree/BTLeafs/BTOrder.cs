using UnityEngine;
using UnityEngine.AI;

[BTAgent(typeof(BTOrder))]
public class BTOrder : BTNode
{
    public override BTResult Execute()
    {
        if (context.contextOwner.didOrder) return BTResult.FAILURE;
        
        ChangeInSatisfactionTypes slow = ChangeInSatisfactionTypes.NO_CHANGE;
        ChangeInSatisfactionTypes fast = ChangeInSatisfactionTypes.NO_CHANGE;
        ChangeInSatisfactionTypes failed = ChangeInSatisfactionTypes.NO_CHANGE;
        UnityEngine.Debug.Log("BTOrder: personalityType = " + context.customer.GetPersonalityType());
        
        switch (context.customer.GetPersonalityType())
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
        }

        context.customer.SetCurrentOutcomesOfAction(
            slow,
            fast,
            failed
        );

        context.orderManager.CreateOrder();
        context.contextOwner.WaitingForOrder();
        context.contextOwner.didOrder = true;
        
        return BTResult.SUCCESS;
    }
}
