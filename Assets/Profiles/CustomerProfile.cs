using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FuzeStudios.Variables;

[CreateAssetMenu(fileName = "CustomerProfile", menuName = "Customer", order = 0)]
public class CustomerProfile : ScriptableObject
{
    [HideInInspector]
    public string name;
    
    public ListOfStringsVariable availableNamesList;

    public FloatVariable satisfaction;

    public FloatVariable maxWaitTime;

    public PersonalityTypeVariable personalityType;

    [Header("Personality Settings")]
    [Range(0, 1)]
    public int neuroticism;
    [Range(0, 1)]
    public int extraversion;
}
