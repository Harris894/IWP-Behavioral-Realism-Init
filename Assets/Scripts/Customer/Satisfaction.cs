using System.Text.RegularExpressions;
using System;
using UnityEngine;

public class Satisfaction
{
    public Satisfaction(float level)
    {
        this.Level = level;
    }

    public float Level { get; private set; }

    public void UpdateCurrentLevel(bool increase, int amount)
    {
        int level = Mathf.RoundToInt(this.Level * 100f);

        if (increase)
        {
            this.Level = (level + amount) / 100f;
            UnityEngine.Debug.Log("Satisfaction: level increased = " + level + "; amount = " + amount);
        }
        else
        {
            this.Level = (level - amount) / 100f;
            UnityEngine.Debug.Log("Satisfaction: level reduced = " + level + "; amount = " + amount);
        }
    }

    public int GetPresentableLevel()
    {
        // If level falls below 10, ceil the result instead of flooring it.
        if (this.Level < 0.1f && this.Level > 0.0f)
            return (int) (Mathf.Ceil(this.Level * 10.0f) * 10);
        
        return (int) (Mathf.Floor(this.Level * 10.0f) * 10);
    }
}