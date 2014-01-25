using UnityEngine;
using System.Collections;

public class Segway : IEvent {
	public Segway() {}    
	string IEvent.GetName() {
		return "Segway";
	}
	object IEvent.GetData() {
		return null;
	}
}
