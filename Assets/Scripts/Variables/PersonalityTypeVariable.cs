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
    public class PersonalityTypeVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public PersonalityTypes Value;

        public void SetValue(PersonalityTypes newValue)
        {
            Value = newValue;
        }

        public void SetValue(PersonalityTypeVariable newValue)
        {
            Value = newValue.Value;
        }
    }
}