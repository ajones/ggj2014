using UnityEngine;
using System.Collections;

public class BabyFedEvent : IEvent {
	public BabyFedEvent() {}    
	string IEvent.GetName() {
		return "BabyFedEvent";
	}
	object IEvent.GetData() {
		return null;
	}
}