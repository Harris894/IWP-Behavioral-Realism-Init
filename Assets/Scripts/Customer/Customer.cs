using System.Collections.Generic;
using System;
using System.Diagnostics;
using UnityEngine;
using FuzeStudios.Variables;

public class Customer : MonoBehaviour
{
    public CustomerRuntimeSet RuntimeSet;
    public CustomerProfile profile;

    private SatisfactionUI satisfUI;

    internal Satisfaction satisfaction;
    
    private bool maxWaitTimePassed = false;
    private int maxWaitTimePassedCounter = 0;

    private int currentSlowActionChange = 0;
    private int currentFastActionChange = 0;
    private int currentFailedActionChange = 0;

    private void Awake()
    {
        satisfaction = new Satisfaction(profile.satisfaction.Value);
        satisfUI = GetComponentInChildren<SatisfactionUI>();


        int randomIndex = UnityEngine.Random.Range(0, profile.availableNamesList.List.Count);
        string randomName = profile.availableNamesList.GetValueAt(randomIndex);
        profile.name = randomName;
    }

    private void OnEnable()
    {
        RuntimeSet.Add(this);
    }

    private void OnDisable()
    {
        RuntimeSet.Remove(this);
    }

    public float GetSatisfactionLevel()
    {
        return satisfaction.Level;
    }

    public float GetPrintableSatisfactionLevel()
    {
        return satisfaction.GetPresentableLevel();
    }

    public float GetMaxWaitTime()
    {
        return profile.maxWaitTime.Value;
    }

    public string GetName()
    {
        return profile.name;
    }

    public PersonalityTypes GetPersonalityType()
    {
        return profile.personalityType.Value;
    }

    /// <summary>
    /// Update the data containing the outcomes of the current action.
    /// </summary>
    public void SetCurrentOutcomesOfAction(
        ChangeInSatisfactionTypes slow,
        ChangeInSatisfactionTypes fast,
        ChangeInSatisfactionTypes failed
    ) {
        currentSlowActionChange = (int) slow;
        currentFastActionChange = (int) fast;
        currentFailedActionChange = (int) failed;
    }

    /// <summary>
    /// Update the satisfaction level based on the provided wait time.
    /// It also provides 2 callbacks - `OnSatisfactionIncrease` and `OnSatisfactionDecrease`.
    /// </summary>
    /// <param name="waitTime">The amount of time it took to complete the previous action</param>
    /// <param name="actionFailed">Whether the current action was failed - wrong order, no bill, etc.</param>
    public void UpdateSatisfactionLevel(float waitTime, bool actionFailed = false)
    {
        if (actionFailed)
        {
            satisfaction.UpdateCurrentLevel(false, currentFailedActionChange);
            OnSatisfactionDecrease();
        }
        else
        {
            // If the customer waits more than the max wait time update the satisfaction level.
            // Also, call the necessary callbacks and set update the necessary flags.
            if (waitTime <= 0.0f)
            {
                satisfaction.UpdateCurrentLevel(false, currentSlowActionChange);
                OnSatisfactionDecrease();

                maxWaitTimePassed = true;
                maxWaitTimePassedCounter++;
            }
            
            // If the customer hasn't waited more than the max wait time.
            if (!maxWaitTimePassed && maxWaitTimePassedCounter <= 0)
            {
                satisfaction.UpdateCurrentLevel(true, currentFastActionChange);

                ResetCurrentOutcomesOfAction();
                OnSatisfactionIncrease();
            }
            // If the customer has waited more than the max wait time more than once update their state.
            else if (maxWaitTimePassed && maxWaitTimePassedCounter > 0)
            {
                OnMaxWaitTimePassed();
            }
        }

        UnityEngine.Debug.Log("Customer: satisfaction = " + GetSatisfactionLevel() + "; " + GetPrintableSatisfactionLevel());
    }

    private void OnSatisfactionIncrease()
    {
        satisfUI.SetSatisfaction(GetPrintableSatisfactionLevel());
        // TODO: Play sounds, change facial expressions.
    }

    private void OnSatisfactionDecrease()
    {
        satisfUI.SetSatisfaction(GetPrintableSatisfactionLevel());
        // TODO: Play sounds, change facial expressions.
    }

    /// <summary>
    /// Trigger the necessary animations and expressions after waiting more than the max wait time, more than once.
    /// </summary>
    private void OnMaxWaitTimePassed()
    {
        // TODO: Play sounds, change facial expressions.
    }

    /// <summary>
    /// Reset the data containing the outcomes of the current action.
    /// </summary>
    private void ResetCurrentOutcomesOfAction() {
        currentSlowActionChange = 0;
        currentFastActionChange = 0;
        currentFailedActionChange = 0;
    }
}
