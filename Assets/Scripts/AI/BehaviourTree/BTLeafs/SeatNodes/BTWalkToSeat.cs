using UnityEngine;

[BTSeat(typeof(BTWalkToSeat))]

public class BTWalkToSeat : BTNode
{
    /// <summary>
    /// Move the agent towards the active seat. Then trigger the `OnSeatReached` event of the seat.
    /// </summary>
    /// <returns>Success if </returns>
    public override BTResult Execute()
    {
        BTResult result = BTResult.SUCCESS;

        Seat currentSeat = context.activeSeat;

        Vector3 agentPosition = context.contextOwner.transform.position;
        Vector3 seatPosition = currentSeat.GetPosition();

        agentPosition.y = 0;
        seatPosition.y = 0;

        if (currentSeat.IsAvailable())
        {
            if ((agentPosition - seatPosition).sqrMagnitude < 0.1f)
            {
                currentSeat.OnSeatReached();
            }
        }
        else result = BTResult.FAILURE;

        return result;
    }
}
