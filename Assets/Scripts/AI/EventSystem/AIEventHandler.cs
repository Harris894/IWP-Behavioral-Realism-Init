using System.Diagnostics;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class AIEventHandler
{
    [SerializeField] bool canHandleEvents = true;
    Animator animatorController;
    NavMeshAgent navAgent;
    AIComponent aiComponent;
    Customer customerScript;

    List<TargetHitInfo> unprocessedTalkInfoList = new List<TargetHitInfo>();
    AIEventSystem eventSystem;

    public void Initialize(AIComponent _aiComponent, Customer _customerScript, Animator _animator, NavMeshAgent _navAgent)
    {
        if (canHandleEvents)
        {
            aiComponent = _aiComponent;
            customerScript = _customerScript;
            animatorController = _animator;
            navAgent = _navAgent;
            eventSystem = AIEventSystem.GetInstance();
            eventSystem.aiGroupEvent += OnEvent;
        }
    }

    internal void Update()
    {
        if (unprocessedTalkInfoList.Count != 0)
        {
            ProcessHits();
        }
    }

    void ProcessHits()
    {
        animatorController.SetTrigger("Hurt");
        aiComponent.currentState = AIState.REACTING;
        navAgent.ResetPath();

        foreach (TargetHitInfo _hitInfo in unprocessedTalkInfoList)
        {
            aiComponent.currentTarget = (IDestination) _hitInfo.hitSource;
            eventSystem.PropagateEvents(aiComponent, _hitInfo.hitSource, StimType.HURT, StimType.THREATENING_SOUND);
        }

        unprocessedTalkInfoList.Clear();
    }

    public void ChangeCurrentState(AIState newState)
    {
        aiComponent.currentState = newState;
        UnityEngine.Debug.Log("AIEventHandler: aiComponent.currentState = " + aiComponent.currentState);
    }

    public void OnTargetHit(TargetHitInfo _talkInfo)
    {
        if (canHandleEvents)
        {
            unprocessedTalkInfoList.Add(_talkInfo);
        }
    }

    void OnEvent(AIEventData _event)
    {
        if (IsValidEvent(_event))
        {
            switch (_event.stimType)
            {
                case StimType.PLAYER_TALKING:
                    if (aiComponent.currentState != AIState.LISTENING)
                    {
                        aiComponent.currentState = AIState.LISTENING;
                    }

                    break;
                default:
                    break;
            }
        }
    }

    private bool IsValidEvent(AIEventData _event)
    {
        bool isValid = false;

        if (_event.sourceAgent != aiComponent as IEventSource)
        {
            switch (_event.stimType)
            {
                default:
                    isValid = (_event.sourcePosition - aiComponent.transform.position).sqrMagnitude < _event.radius * _event.radius;
                    break;
            }
        }

        return isValid;
    }

    internal void OnDestroy()
    {
        if (eventSystem != null)
        {
            eventSystem.aiGroupEvent -= OnEvent;
        }
    }
}
