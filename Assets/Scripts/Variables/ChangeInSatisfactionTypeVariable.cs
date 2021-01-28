// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using UnityEngine;

namespace FuzeStudios.Variables
{
    [CreateAssetMenu]
    public class ChangeInSatisfactionTypeVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public ChangeInSatisfactionTypes Value;

        public void SetValue(ChangeInSatisfactionTypes newValue)
        {
            Value = newValue;
        }

        public void SetValue(ChangeInSatisfactionTypeVariable newValue)
        {
            Value = newValue.Value;
        }
    }
}