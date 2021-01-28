using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using XNode;

[CreateAssetMenu]
public class Example : NodeGraph { 
	public class StateNode : Node {
		[Input] public float a;
		[Output] public float b;

		public override object GetValue(NodePort port) {
			if (port.fieldName == "b") return GetInputValue<float>("a", a);
			else return null;
		}
	}
}