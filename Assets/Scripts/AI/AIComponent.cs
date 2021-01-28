using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEditor;
using TMPro;
using FuzeStudios.Variables;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class AIComponent : MonoBehaviour, IEventSource, IFreezable
{
    public BehaviourTreeType behaviourTreeType;
    public SensorySystem sensorySystem;
    public AIEventHandler eventHandler;
    public ScoreSystem _scoreSystem;

    [SerializeField]
    public IntegerVariable _consumptionTime;
    [SerializeField]
    private GameObject takeOrderButton;
    [SerializeField]
    private GameObject messageBox;
    [SerializeField]
    private TextMeshProUGUI customersWhoLeft;

    internal Seat activeSeat;
    internal AIState currentState = AIState.ENTERING;
    internal MovementState currentMoveState = MovementState.IDLE;
    internal IDestination currentTarget = null;

    internal float waitTime = 0.0f;
    internal bool shouldWait = false;
    internal bool didOrder = false;
    internal bool hadRefill = false;

    Customer customerScript;
    OrderingAction orderManager;
    Animator animatorController;
    NavMeshAgent navAgent;
    BTContext aiContext;
    AudioSource audioSource;
    AudioManager audioManager;
    VHPEmotions emotions;

    /// <summary>
    /// Get a reference to the necessary instances before initializing them.
    /// </summary>
    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animatorController = GetComponent<Animator>();
        customerScript = GetComponent<Customer>();
        orderManager = GetComponent<OrderingAction>();
        audioSource = GetComponent<AudioSource>();
        audioManager = AudioManager.instance;
        emotions = GetComponent<VHPEmotions>();

        aiContext = new BTContext(this, customerScript, orderManager, animatorController, navAgent);
    }

    /// <summary>
    /// Initialize the necessary components that need to be running.
    /// </summary>
    private void Start()
    {
        sensorySystem.Initialize(this, navAgent);
        eventHandler.Initialize(this, customerScript, animatorController, navAgent);
        BehaviourTreeRuntimeData.RegisterAgentContext(behaviourTreeType, aiContext);
    }

    /// <summary>
    /// This method gets initiated when the agent reaches the chosen seat.
    /// Use it to trigger state changes and animations, sounds, etc..
    /// </summary>
    public void OnSeatReached()
    {
        eventHandler.ChangeCurrentState(AIState.WAITING_FOR_BARTENDER);
        takeOrderButton.gameObject.SetActive(true);
        emotions.happiness = 50f;
    }

    /// <summary>
    /// This method gets initiated when the agent needs to listen to the player.
    /// Use it to trigger state changes and animations, sounds, etc..
    /// </summary>
    public void ListenToPlayer()
    {
        if (shouldWait)
        {
            OnWaitEnd();
            UpdateSatisfactionLevel(false);
        }

        eventHandler.ChangeCurrentState(AIState.LISTENING);
    }

    /// <summary>
    /// This method gets initiated when the agent needs to respond to the player.
    /// Use it to trigger state changes and animations, sounds, etc..
    /// </summary>
    public void RespondToPlayer()
    {
        eventHandler.ChangeCurrentState(AIState.REACTING);
    }

    /// <summary>
    /// This method gets initiated when the agent calls the bartender while waiting at the counter.
    /// Use it to trigger state changes and animations, sounds, etc..
    /// </summary>
    public void CallPlayer()
    {
        // TODO: Call the player
        Wait();
    }

    /// <summary>
    /// This method gets initiated when the agent is choosing what to consume.
    /// Use it to trigger state changes and animations, sounds, etc..
    /// </summary>
    public void ChooseMeal()
    {
        if (currentState == AIState.WAITING_FOR_BARTENDER)
        {
            takeOrderButton.gameObject.SetActive(false);

            OnWaitEnd();
            UpdateSatisfactionLevel(true);

            eventHandler.ChangeCurrentState(AIState.CHOOSING_MEAL);

            // TODO: Feedback to the player - animation change/faction expression/sound/etc.
            Order();
        }
    }

    /// <summary>
    /// This method gets initiated when the agent is choosing a refill.
    /// Use it to trigger state changes and animations, sounds, etc..
    /// </summary>
    public void ChooseRefill()
    {
        messageBox.SetActive(false);
        eventHandler.ChangeCurrentState(AIState.CHOOSING_REFILL);
        hadRefill = true;

        // TODO: Feedback to the player - animation change/faction expression/sound/etc.
        Order();
    }

    /// <summary>
    /// This method gets initiated when the agent is ready to order.
    /// Use it to trigger state changes and animations, sounds, etc..
    /// </summary>
    public void Order()
    {
        eventHandler.ChangeCurrentState(AIState.ORDERING);

        // TODO: Feedback to the player - animation change/faction expression/sound/etc.
    }

    /// <summary>
    /// This method gets initiated when the agent is waiting for their order.
    /// Use it to trigger state changes and animations, sounds, etc..
    /// </summary>
    public void WaitingForOrder()
    {
        eventHandler.ChangeCurrentState(AIState.WAITING_FOR_ORDER);

        // TODO: Feedback to the player - animation change/faction expression/sound/etc.
        if (orderManager != null && orderManager.pendingOrders.Count > 0)
        {
            messageBox.SetActive(true);

            TextMeshProUGUI messageText = messageBox.GetComponentInChildren<TextMeshProUGUI>();
            messageText.SetText(orderManager.pendingOrders[0]);
        }
    }

    /// <summary>
    /// This method gets initiated when the agent received their order.
    /// Use it to trigger state changes and animations, sounds, etc..
    /// </summary>
    public void ReceiveOrder(bool orderCorrect)
    {
        if (currentState != AIState.WAITING_FOR_ORDER || !didOrder) return;

        OnWaitEnd();
        UpdateSatisfactionLevel(!orderCorrect);
        if (orderCorrect)
        {
            audioManager.PlayResponseSound("Thanks", audioSource);
        }
        else
        {
            audioManager.PlayResponseSound("Wrong order", audioSource);
            emotions.anger = 50f;
        }

        // TODO: React before starting to eat.
        eventHandler.ChangeCurrentState(AIState.CONSUMING);
        didOrder = false;
        messageBox.GetComponentInChildren<TextMeshProUGUI>().SetText("Consuming...");

        StartCoroutine(ConsumingHardcodedDuration());
    }

    /// <summary>
    /// This method gets initiated when the agent leaves their seat and exits the building.
    /// Use it to trigger state changes and animations, sounds, etc..
    /// </summary>
    public void CustomerLeaves()
    {
        if (currentState != AIState.LEAVING)
        {
            OnWaitEnd();

            // TODO: React before leaving.
            audioManager.PlayOtherSound("Out of here", audioSource);
            eventHandler.ChangeCurrentState(AIState.LEAVING);

            _scoreSystem.UpdateHighScore((int) customerScript.GetPrintableSatisfactionLevel());
        }
    }

    /// <summary>
    /// Whether or not the agent is waiting.
    /// </summary>
    public bool IsWaiting()
    {
        return shouldWait && waitTime > 0.0f;
    }

    /// <summary>
    /// Begin the waiting timer that would determine the end value of the satisfaction level.
    /// </summary>
    public void Wait()
    {
        waitTime = customerScript.GetMaxWaitTime();
        shouldWait = true;
    }

    /// <summary>
    /// Stop the waiting timer and update the satisfaction level.
    /// </summary>
    public void OnWaitEnd()
    {
        waitTime = 0.0f;
        shouldWait = false;
    }

    /// <summary>
    /// This method gets triggered when the max wait time has been passed.
    /// Use it to update the current satisfaction level and notify the necessary gameObjects.
    /// </summary>
    void OnMaxWaitTimePassed()
    {
        UpdateSatisfactionLevel(false);

        if (shouldWait) Wait();
    }

    /// <summary>
    /// Update the current satisfaction level from within the `Customer` script.
    /// Also trigger `LEAVING` state if it reaches 0
    /// </summary>
    void UpdateSatisfactionLevel(bool actionFailed)
    {
        // Agent leaves if their satisfaction level reaches 0.
        if (customerScript.GetSatisfactionLevel() <= 0.0f)
        {
            OnWaitEnd();
            CustomerLeaves();
        }
        else
        {
            customerScript.UpdateSatisfactionLevel(waitTime, actionFailed);
        }
    }

    void Update()
    {
        sensorySystem.Update();
        eventHandler.Update();

        if (shouldWait)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0.0f)
            {
                OnMaxWaitTimePassed();
            }
        }
    }

    void OnDestroy()
    {
        if (customerScript.satisfaction.Level <= 0.0f)
        {
            int currentCustomersWhoLeft = int.Parse(customersWhoLeft.text);
            customersWhoLeft.SetText((currentCustomersWhoLeft + 1).ToString());
        }

        eventHandler.OnDestroy();
        BehaviourTreeRuntimeData.UnregisterAgentContext(behaviourTreeType, aiContext);
    }

    /// <summary>
    /// Get the current position of the agent.
    /// </summary>
    /// <returns>The current Vector3 position of the agent</returns>
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private IEnumerator ConsumingHardcodedDuration()
    {
        yield return new WaitForSeconds(_consumptionTime.Value);

        activeSeat.eatingZone.DestroyObjectInside();
        if (!hadRefill)
        {
            ChooseRefill();
        }
        else
        {
            CustomerLeaves();
        }
    }

    public void Freeze()
    {
        shouldWait = false;
    }

    public void Defrost()
    {
        shouldWait = true;
    }
}
