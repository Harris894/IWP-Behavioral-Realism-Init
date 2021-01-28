// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace FuzeStudios.Variables
{
    [Serializable]
    public class ListOfStringsReference
    {
        public ListOfStringsVariable Variable;

        public ListOfStringsReference()
        { }

        public List<string> Value
        {
            get { return Variable.List; }
        }
    }
}