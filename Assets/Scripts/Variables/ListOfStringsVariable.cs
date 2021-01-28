// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace FuzeStudios.Variables
{
    [CreateAssetMenu]
    public class ListOfStringsVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public List<string> List;

        public void AddValue(string newValue)
        {
            List.Add(newValue);
        }

        public void AddValue(StringVariable newValue)
        {
            List.Add(newValue.Value);
        }

        public void RemoveValue(string valueToRemove)
        {
            List.Remove(valueToRemove);
        }

        public void RemoveValue(StringVariable valueToRemove)
        {
            List.Remove(valueToRemove.Value);
        }

        public string GetValueAt(int index)
        {
            return List[index];
        }

        public void RemoveValueAt(int index)
        {
            List.RemoveAt(index);
        }
    }
}