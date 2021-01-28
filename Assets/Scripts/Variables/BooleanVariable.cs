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
    public class BooleanVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public bool Value;

        public void SetValue(bool value)
        {
            Value = value;
        }

        public void SetValue(BooleanVariable value)
        {
            Value = value.Value;
        }

        public void ApplyChange(bool amount)
        {
            Value = amount;
        }

        public void ApplyChange(BooleanVariable amount)
        {
            Value = amount.Value;
        }
    }
}