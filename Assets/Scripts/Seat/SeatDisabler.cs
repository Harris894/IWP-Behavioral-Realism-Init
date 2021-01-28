using UnityEngine;

public class SeatDisabler : MonoBehaviour
{
    public SeatRuntimeSet Set;

    public void DisableSeat(Seat seat)
    {
        Set.Remove(seat);
    }
}
