using UnityEngine;

[BTEntrance(typeof(BTWalkToEntrance))]

public class BTWalkToEntrance : BTNode
{
    /// <summary>
    /// Move the agent towards the active entrance. Then trigger the `OnEntranceReached` event of the entrance.
    /// </summary>
    /// <returns>Success if </returns>
    public override BTResult Execute()
    {
        BTResult result = BTResult.SUCCESS;

        Entrance entrance = Entrance.GetInstance();

        Vector3 agentPosition = context.contextOwner.transform.position;
        Vector3 entrancePosition = entrance.GetPosition();

        agentPosition.y = 0;
        entrancePosition.y = 0;

        if ((agentPosition - entrancePosition).sqrMagnitude < 0.1f)
        {
            entrance.OnEntranceReached();
        }

        return result;
    }
}
