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
    public class BooleanReference
    {
        public bool UseConstant = true;
        public bool ConstantValue;
        public BooleanVariable Variable;

        public BooleanReference()
        { }

        public BooleanReference(bool value)
        {
            UseConstant = true;
            ConstantValue = value;
        }

        public bool Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
        }

        public static implicit operator bool(BooleanReference reference)
        {
            return reference.Value;
        }
    }
}