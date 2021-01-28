using System;
using System.Reflection;
using static XNode.Node;

public static class BTDefs
{
    public const string stpMenuName = "SmartTerrainPoint";
    public const string MOVEMENT_STATE = "MoveState";

}


public enum MovementState
{
    IDLE,
    WALK,
}

public enum BTResult
{
    SUCCESS,
    FAILURE,
    XRUNNING_DO_NOT_USE // LEAF NODES SHOULD NEVER RETURN RUNNING! BEHAVIOUR TREE IS NOT MULTITHREADED
}

//Add new types to the bottom of this enum, before COUNT
//Failing to do so will reorder the enum and mess up the values you set in the scene!!
public enum BehaviourTreeType
{
    CUSTOMER,
    //Add stuff here
    COUNT,
}

public enum HasOp
{
    PATH,
    PATH_TO_TARGET,
    TARGET,
    STP,
    SEAT,
}

public enum IsOp
{
    WALKING,
    ENTERING,
    WAITING_FOR_BARTENDER,
    WAITING_FOR_ORDER,
    CHOOSING_MEAL,
    ORDERING,
    LISTENING,
    REACTING,
    CONSUMING,
    LEAVING
}

public enum PathType
{
    TARGET,
    RANDOM
}

public class BTCompositeAttribute : CreateNodeMenuAttribute
{
    public BTCompositeAttribute(Type _type) : base("test")
    {
        menuName = "Composites/" + _type.ToString();
    }
}

public class BTAgentAttribute : CreateNodeMenuAttribute
{    
    public BTAgentAttribute(Type _type) : base("test")
    {
        menuName = "Agent/" + _type.ToString();
    }
}

public class BTSmartTerrainPointAttribute : CreateNodeMenuAttribute
{
    public BTSmartTerrainPointAttribute(Type _type) : base("test")
    {
        menuName = "SmartTerrainPoint/" + _type.ToString();
    }
}

public class BTDestinationAttribute : CreateNodeMenuAttribute
{
    public BTDestinationAttribute(Type _type) : base("test")
    {
        menuName = "Destination/" + _type.ToString();
    }
}

public class BTEntranceAttribute : CreateNodeMenuAttribute
{
    public BTEntranceAttribute(Type _type) : base("test")
    {
        menuName = "Entrance/" + _type.ToString();
    }
}

public class BTSeatAttribute : CreateNodeMenuAttribute
{
    public BTSeatAttribute(Type _type) : base("test")
    {
        menuName = "Seat/" + _type.ToString();
    }
}
