using UnityEngine;

public interface IDestination
{
    Vector3 GetPosition();
    
    void OnDestinationReached();
}
