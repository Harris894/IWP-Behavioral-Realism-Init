using UnityEngine.AI;

[BTSeat(typeof(BTFindAndSelectSeat))]
public class BTFindAndSelectSeat : BTNode
{
    public override BTResult Execute()
    {
        BTResult result = BTResult.FAILURE;

        if (context.contextOwner.activeSeat) return result;

        Seat seat = SeatManager.SelectNearestReachableSeat(context.contextOwner, out NavMeshPath _path);
        if (seat != null)
        {
            context.contextOwner.activeSeat = seat;
            context.contextOwner.currentTarget = seat;
            context.navAgent.SetPath(_path);
            
            result = BTResult.SUCCESS;
        }

        return result;
    }
}
