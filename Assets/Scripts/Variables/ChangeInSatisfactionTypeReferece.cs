// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using System;

namespace FuzeStudios.Variables
{
    [Serializable]
    public class ChangeInSatisfactionTypeReferece
    {
        public ChangeInSatisfactionTypeVariable Variable;

        public ChangeInSatisfactionTypeReferece()
        { }

        public ChangeInSatisfactionTypes Value
        {
            get { return Variable.Value; }
        }
    }
}