using UnityEngine;
using UnityEngine.AI;

[BTAgent(typeof(BTAgentWait))]
public class BTAgentWait : BTNode
{
    public ChangeInSatisfactionTypes slowChangeInSatisfaction;
    public ChangeInSatisfactionTypes fastChangeInSatisfaction;
    public ChangeInSatisfactionTypes failedChangeInSatisfaction;

    public override BTResult Execute()
    {
        if (context.contextOwner.IsWaiting()) return BTResult.SUCCESS;

        context.customer.SetCurrentOutcomesOfAction(
            slowChangeInSatisfaction,
            fastChangeInSatisfaction,
            failedChangeInSatisfaction
        );

        context.contextOwner.Wait();
        
        return BTResult.SUCCESS;
    }
}
