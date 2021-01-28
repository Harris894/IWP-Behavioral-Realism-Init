[BTAgent(typeof(BTSetAgentSpeed))]
public class BTSetAgentSpeed : BTNode
{
    public float desiredSpeed;
    public override BTResult Execute()
    {
        BTResult result = BTResult.SUCCESS;
        if (context.navAgent.speed == desiredSpeed) return result;

        context.navAgent.speed = desiredSpeed;
        return result;
    }
}
