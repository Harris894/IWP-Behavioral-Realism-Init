using UnityEngine;

[BTSeat(typeof(BTSeatReached))]
public class BTSeatReached : BTNode
{
    public override BTResult Execute()
    {
        BTResult result = BTResult.FAILURE;

        Seat currentSeat = context.activeSeat;

        Vector3 agentPosition = context.contextOwner.transform.position;
        Vector3 seatPosition = currentSeat.transform.position;

        agentPosition.y = 0;
        seatPosition.y = 0;

UnityEngine.Debug.Log((agentPosition - seatPosition).sqrMagnitude);
        if (currentSeat.IsAvailable() && (agentPosition - seatPosition).sqrMagnitude <= 0.1f)
        {
            result = BTResult.SUCCESS;
        }

        return result;
    }
}
