﻿using UnityEngine;

public class TargetHitInfo
{
    public TargetHitInfo(IEventSource _hitSource)
    {
        hitSource = _hitSource;
    }

    public IEventSource hitSource;
}
