using UnityEngine;

[BTAgent(typeof(BTDestinationReached))]

public class BTDestinationReached : BTNode
{
    /// <summary>
    /// Move the agent towards the active destination. Then trigger the `OnDestinationReached` event of the destination.
    /// </summary>
    /// <returns>Success if </returns>
    public override BTResult Execute()
    {
        BTResult result = BTResult.FAILURE;

        IDestination destination = context.contextOwner.currentTarget;

        if (destination == null) return result;

        Vector3 agentPosition = context.contextOwner.GetPosition();
        Vector3 destinationPosition = destination.GetPosition();

        agentPosition.y = 0;
        destinationPosition.y = 0;

        //UnityEngine.Debug.Log("BTDestinationReached: sqrMagnitude = " + (agentPosition - destinationPosition).sqrMagnitude);

        if ((agentPosition - destinationPosition).sqrMagnitude < 0.1f)
        {
            destination.OnDestinationReached();

            context.navAgent.ResetPath();
            context.navAgent.isStopped = true;

            context.contextOwner.currentTarget = null;
            PlayerController playerController = PlayerController.GetInstance();
            //var rotation = Quaternion.LookRotation(
            //   playerController.GetPosition() - context.contextOwner.transform.position
            //);

            context.contextOwner.transform.LookAt(playerController.transform.position);

            //context.contextOwner.transform.rotation = Quaternion.Slerp(
            //   context.contextOwner.transform.rotation, rotation, Time.deltaTime * 6.0f
            //);

            result = BTResult.SUCCESS;
        }

        return result;
    }
}
