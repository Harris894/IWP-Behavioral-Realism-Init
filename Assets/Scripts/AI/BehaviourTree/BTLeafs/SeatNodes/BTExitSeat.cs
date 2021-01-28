using System;
[BTSeat(typeof(BTExitSeat))]
public class BTExitSeat : BTNode
{
    public override BTResult Execute()
    {
        SeatManager.OnSeatExit(context.contextOwner.activeSeat);

        return BTResult.SUCCESS;
    }
}
