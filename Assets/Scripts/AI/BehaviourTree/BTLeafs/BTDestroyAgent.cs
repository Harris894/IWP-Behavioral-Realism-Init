[BTAgent(typeof(BTDestroyAgent))]
public class BTDestroyAgent : BTNode
{
    public override BTResult Execute()
    {
        Destroy(context.contextOwner.gameObject);
        return BTResult.SUCCESS;
    }
}
